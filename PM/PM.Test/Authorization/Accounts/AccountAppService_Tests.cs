using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.TestBase;
using EntityFramework.DynamicFilters;
using PM.Application.Authorization.Accounts;
using PM.Application.Authorization.Accounts.Dto;
using Shouldly;
using Xunit;

namespace PM.Test.Authorization.Accounts
{
    public class AccountAppService_Tests: PMTestBase
    {
        private readonly IAccountAppService _accountAppService;

        public AccountAppService_Tests()
        {
            //创建被测试的类（SUT（Software Under Test） - 被测系统）
            _accountAppService = LocalIocManager.Resolve<IAccountAppService>();
        }
        [Fact]
        public async Task IsTenantAvaliableForDefault()
        {
            var isTenantAvaliable =
                await _accountAppService.IsTenantAvaliable(new IsTenantAvaliableInput() {TenantName = "Default"});
            isTenantAvaliable.State.ShouldBe(TenantAvaliablityState.Avaliable);
        }
        [Fact]
        public async Task IsTenantAvaliableForDefault1()
        {
            var isTenantAvaliable =
                await _accountAppService.IsTenantAvaliable(new IsTenantAvaliableInput() { TenantName = "Default1" });
            isTenantAvaliable.State.ShouldBe(TenantAvaliablityState.NotFound);
        }


    }
}
