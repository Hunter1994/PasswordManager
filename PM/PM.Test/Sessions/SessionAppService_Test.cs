using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM.Application.Sessions;
using Shouldly;
using Xunit;

namespace PM.Test.Sessions
{
    public class SessionAppService_Test:PMTestBase
    {
        private readonly ISessionAppService sessionAppService;

        public SessionAppService_Test()
        {
            sessionAppService = LocalIocManager.Resolve<ISessionAppService>();
        }
        [Fact]
        public async Task GetCurrentLoginInformations_Null()
        {
            var sesstion = await sessionAppService.GetCurrentLoginInformations();

            var currentUser = await GetCurrentUserAsync();
            var currentTenant = await GetCurrentTenantAsync();
            sesstion.User.ShouldNotBeNull();
            sesstion.Tenant.ShouldNotBeNull();
            sesstion.User.Name.ShouldBe(currentUser.Name);
            sesstion.Tenant.Name.ShouldBe(currentTenant.Name);
        }
        [Fact]
        public async Task GetCurrentLoginInformations_LoginHost()
        {
            LoginAsHostAdmin();
            var sesstion = await sessionAppService.GetCurrentLoginInformations();

            var currentUser = await GetCurrentUserAsync();
            sesstion.User.ShouldNotBe(null);
            sesstion.User.Name.ShouldBe(currentUser.Name);
            sesstion.User.Surname.ShouldBe(currentUser.Surname);
            sesstion.Tenant.ShouldBeNull();
        }
    }
}
