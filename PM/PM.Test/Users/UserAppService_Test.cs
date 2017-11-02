using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM.Application.Users;
using PM.Application.Users.Dto;
using Shouldly;
using Xunit;

namespace PM.Test.Users
{
    public class UserAppService_Test:PMTestBase
    {
        private readonly IUserAppService _userAppService;
        public UserAppService_Test()
        {
            _userAppService = LocalIocManager.Resolve<IUserAppService>();
        }
        [Fact]
        public async Task Create()
        {

            var userDto = new CreateUserDto();
            userDto.Name = "张三";
            userDto.UserName = "zhangsan";
            userDto.IsActive = true;
            userDto.EmailAddress = "123@qq.com";
            userDto.Surname = "张";
            userDto.RoleNames = new string[] { "Admin"};
            userDto.Password = "1";
            var user = await _userAppService.Create(userDto);
            user.ShouldNotBeNull();
            user.UserName.ShouldBe("zhangsan");
        }

    }
}
