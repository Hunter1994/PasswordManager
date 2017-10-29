using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Zero.Configuration;
using PM.Application.Authorization.Accounts.Dto;
using PM.Core.Authorization.Users;
using Abp.Configuration;
namespace PM.Application.Authorization.Accounts
{
    public class AccountAppService:PMAppServiceBase,IAccountAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;

        public AccountAppService(UserRegistrationManager userRegistrationManager)
        {
            _userRegistrationManager = userRegistrationManager;
        }

        public async Task<IsTenantAvaliableOutput> IsTenantAvaliable(IsTenantAvaliableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenantName);
            if (tenant == null)
            {
                return new IsTenantAvaliableOutput(TenantAvaliablityState.NotFound);
            }
            if (!tenant.IsActive)
            {
                return new IsTenantAvaliableOutput(TenantAvaliablityState.InActive);
            }
            return new IsTenantAvaliableOutput(TenantAvaliablityState.Avaliable, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user =await _userRegistrationManager.RegisterAsync(input.Name, input.Surname, input.EmailAddress,
                input.UserName,
                input.Password, false);

            //是电子邮件确认需要登录
            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(
                AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput()
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
    }
}
