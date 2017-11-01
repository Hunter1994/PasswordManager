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
           var grantedPermissions= PermissionManager.GetAllPermissions().Where(p => input.Permissions.Contains(p.Name)).ToList();



        }


        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            throw new NotImplementedException();
        }


        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
