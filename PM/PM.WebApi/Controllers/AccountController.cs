using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using PM.Core;
using PM.Core.Authorization;
using PM.Core.MultiTenant;
using PM.Core.Users;
using PM.WebApi.Models;

namespace PM.WebApi.Controllers
{
    public class AccountController:AbpApiController
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        private readonly LogInManager _logInManager;
        static AccountController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }

        public AccountController(LogInManager logInManager)
        {
            _logInManager = logInManager;
            LocalizationSourceName = PMProjectNameConsts.LocalizationSourceName;
        }

        public async Task<AjaxResponse> Authenticate(LoginModel loginModel)
        {
            CheckModelState();
            var loginResult =
                await
                    GetLoginResultAsync(
                        loginModel.UsernameOrEmailAddress,
                        loginModel.Password,
                        loginModel.TenancyName);
            //票据
            var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;//发行时间
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));//到期时间
            return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));

        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress,
            string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }


        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress,
            string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new UserFriendlyException("不要用这个方法调用成功的结果！");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress://无效用户名或电子邮箱
                case AbpLoginResultType.InvalidPassword://无效密码
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName://无效的租户名称
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive://租户未激活
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive://用户没激活
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed://用户邮箱未确认
                    return new UserFriendlyException(L("LoginFailed"), "您的电子邮件地址未被确认。 你不能登录"); //TODO: 本地化消息
                default: //实际上不能落到默认值。 但是其他结果类型可以在将来添加，我们可能会忘记处理它
                    Logger.Warn("未处理的登录失败原因: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("无效的请求！");
            }
        }


    }
}
