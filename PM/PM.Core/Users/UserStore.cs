using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using PM.Core.Authorization.Roles;

namespace PM.Core.Users
{
    public class UserStore:AbpUserStore<Role,User>
    {
        public UserStore(
            IRepository<User, long> userRepository, 
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository, 
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager, 
            IRepository<UserClaim, long> userClaimRepository)
            : base(
                userRepository, 
                userLoginRepository, 
                userRoleRepository, 
                roleRepository, 
                userPermissionSettingRepository,
                unitOfWorkManager, 
                userClaimRepository)
        {
        }
    }
}
