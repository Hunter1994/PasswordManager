using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.UI;
using Microsoft.AspNet.Identity;
using PM.Application.Roles.Dto;
using PM.Core.Authorization.Roles;
using PM.Core.MultiTenant;
using PM.Core.Users;

namespace PM.Application.Roles
{
    public class RoleAppService:AsyncCrudAppService<Role,RoleDto,Int32,PagedResultRequestDto,CreateRoleDto,RoleDto>,IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository; 
        public RoleAppService(
            IRepository<Role, int> repository,
            RoleManager roleManager,
            UserManager userManager,
            IRepository<User,long> userRepository,
            IRepository<UserRole,long> userRoleRepository,
            IRepository<Role> roleRepository    
            ) : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = input.MapTo<Role>();

            CheckErrors(await _roleManager.CreateAsync(role));

            //查询权限
            var grantedPermissions =
                PermissionManager.GetAllPermissions().Where(p => input.Permissions.Contains(p.Name)).ToList();

            //给角色设置权限
            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id);
            if (role.IsStatic)
            {
                throw new UserFriendlyException("无法删除静态角色");
            }

            //删除用户角色关联
            var users = await GetUsersInRoleAsync(role.Name);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.Name));
            }

            //删除角色
            await _roleManager.DeleteAsync(role);
        }


        private Task<List<long>> GetUsersInRoleAsync(string roleName)
        {
           var users= (from user in _userRepository.GetAll()
                join userRole in _userRoleRepository.GetAll() on user.Id equals userRole.UserId
                join role in _roleRepository.GetAll() on userRole.RoleId equals role.Id
                where role.Name == roleName
                select user.Id).Distinct().ToList();
            return Task.FromResult(users);
        }


        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();
            return Task.FromResult(new ListResultDto<PermissionDto>(permissions.MapTo<List<PermissionDto>>()));
        }

        protected override Task<Role> GetEntityByIdAsync(int id)
        {
            //查询角色，并包含该角色的权限
            var role = Repository.GetAllIncluding(x => x.Permissions).FirstOrDefault(x => x.Id == id);
            return Task.FromResult(role);
        }
        /// <summary>
        /// 创建过滤查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Role> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions);
        }

        /// <summary>
        /// 应用排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }
        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
