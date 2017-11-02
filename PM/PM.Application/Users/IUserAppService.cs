using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PM.Application.Roles.Dto;
using PM.Application.Users.Dto;

namespace PM.Application.Users
{
    public interface IUserAppService:IAsyncCrudAppService<UserDto,long,PagedResultRequestDto,CreateUserDto,UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}
