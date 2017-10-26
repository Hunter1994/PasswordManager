using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using PM.Core.Authorization.Roles;

namespace PM.Core.Users
{
    public class UserManager : AbpUserManager<Role, User>
    {
        public UserManager(
            UserStore userStore, 
            RoleManager roleManager,
            IPermissionManager permissionManager, 
            IUnitOfWorkManager unitOfWorkManager, 
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings, 
            ILocalizationManager localizationManager,
            IdentityEmailMessageService emailService, 
            ISettingManager settingManager,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                userStore, 
                roleManager, 
                permissionManager, 
                unitOfWorkManager, 
                cacheManager, 
                organizationUnitRepository,
                userOrganizationUnitRepository, 
                organizationUnitSettings, 
                localizationManager, 
                emailService,
                settingManager, 
                userTokenProviderAccessor)
        {
        }
    }
}
