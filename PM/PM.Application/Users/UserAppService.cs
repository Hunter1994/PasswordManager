using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Microsoft.AspNet.Identity;
using PM.Application.Roles.Dto;
using PM.Application.Users.Dto;
using PM.Core.Authorization.Roles;
using PM.Core.Users;

namespace PM.Application.Users
{
    public class UserAppService:AsyncCrudAppService<User,UserDto,long,PagedResultRequestDto,CreateUserDto,UpdateUserDto>,IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly RoleManager _roleManager;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            IRepository<Role> roleRepository,
            RoleManager roleManager 
            ) : base(repository)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
        }

        public override async Task<UserDto> Get(EntityDto<long> input)
        {
            var user = await base.Get(input);
            var userRoles = await _userManager.GetRolesAsync(user.Id);
            user.Roles = userRoles.ToArray();
            return user;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();
            var user = input.MapTo<User>();
            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //分配角色（从租户的所有）
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }
            CheckErrors(await _userManager.CreateAsync(user));
            return MapToEntityDto(user);
        }

        public override async Task<UserDto> Update(UpdateUserDto input)
        {
            CheckUpdatePermission();
            var user = await _userManager.GetUserByIdAsync(input.Id);

            //把有变动的属性赋值到user对象中
            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                //_userManager.SetRoles方法的作用：变更角色（前提：用户表以已经创建）
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);

            //admin用户不能被删除（删除用户的同时会删除UserRole）
            await _userManager.DeleteAsync(user);
        }
        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(roles.MapTo<List<RoleDto>>());
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            return user;
        }

        protected override void MapToEntity(UpdateUserDto updateInput, User entity)
        {
            ObjectMapper.Map(updateInput, entity);
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }
        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
