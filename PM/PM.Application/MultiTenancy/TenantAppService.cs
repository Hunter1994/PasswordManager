using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Microsoft.AspNet.Identity;
using PM.Application.MultiTenancy.Dto;
using PM.Core.Authorization.Roles;
using PM.Core.Editions;
using PM.Core.MultiTenant;
using PM.Core.Users;

namespace PM.Application.MultiTenancy
{
    public class TenantAppService :  AsyncCrudAppService<Tenant, TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        public TenantAppService(
            IRepository<Tenant, int> repository,
            TenantManager tenantManager,
            EditionManager editionManager,
            UserManager userManager,
            RoleManager roleManager,
            IAbpZeroDbMigrator abpZeroDbMigrator
            ) : base(repository)
        {
            _editionManager = editionManager;
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
        }

        private void CheckError(IdentityResult identityResult)
        {
            identityResult.CheckErrors();
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<TenantDto> Create(CreateTenantDto input)
        {
            //判断是否已拥有此接口的权限（Create方法），需要赋值CreatePermissionName属性
            CheckCreatePermission();

            var tenant = input.MapTo<Tenant>();

            //加密数据库链接字符串（采用AES对称加密）
            tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                ? null
                : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

            //当前租户使用的版本（Standard标准版）
            var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                tenant.EditionId = defaultEdition.Id;
            }

            //创建租户
            await _tenantManager.CreateAsync(tenant);
            //获得租户的id
            await CurrentUnitOfWork.SaveChangesAsync();

            //创建租户数据库
            _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                //创建静态租户角色，该静态角色通过IRoleManagementConfig配置
                CheckError(await _roleManager.CreateStaticRoles(tenant.Id));
                await CurrentUnitOfWork.SaveChangesAsync();//获取静态角色id

                //授予管理员角色所有权限（该权限通过IPermissionDefinitionContext配置）
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                //创建租户admin用户
                var adminUser= User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress, User.DefaultPassword);
                CheckError(await _userManager.CreateAsync(adminUser));
                await CurrentUnitOfWork.SaveChangesAsync();//获取用户id

                //讲角色分配给租户admin用户
                CheckError(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return MapToEntityDto(tenant);
        }

        protected override void MapToEntity(TenantDto updateInput, Tenant entity)
        {
            //手动映射，因为TenantDto也包含不可编辑的属性。
            entity.Name = updateInput.Name;
            entity.TenancyName = updateInput.TenancyName;
            entity.IsActive = updateInput.IsActive;
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();
            var tenant =await _tenantManager.FindByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }
    }
}
