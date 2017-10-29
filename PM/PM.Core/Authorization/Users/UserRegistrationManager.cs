using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNet.Identity;
using PM.Core.Authorization.Roles;
using PM.Core.MultiTenant;
using PM.Core.Users;

namespace PM.Core.Authorization.Users
{
    public class UserRegistrationManager:DomainService
    {
        public IAbpSession AbpSession { get; set; }
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public UserRegistrationManager(TenantManager tenantManager, UserManager userManager, RoleManager roleManager)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName,
            string plainPassword, bool isEmailConfirmed)
        {
            CheckForTenant();
            var tenant = await GetActiveTenantAsync();

            var user = new User()
            {
                TenantId = tenant.Id,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                IsEmailConfirmed = isEmailConfirmed,
                Roles = new List<UserRole>()
            };

            user.Password = new PasswordHasher().HashPassword(plainPassword);

            foreach (var defaultRoles in _roleManager.Roles.Where(r=>r.IsDefault).ToList())
            {
                user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRoles.Id));
            }

            CheckError(await _userManager.CreateAsync(user));

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private void CheckForTenant()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new InvalidOperationException("无法注册主机用户！");
            }
        }

        private async Task<Tenant> GetActiveTenantAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }
            return await GetActiveTenantAsync(AbpSession.TenantId.Value);
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }
            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }
            return tenant;
        }

        protected virtual void CheckError(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

    }
}
