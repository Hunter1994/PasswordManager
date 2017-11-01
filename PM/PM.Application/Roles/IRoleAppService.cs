using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PM.Application.Roles.Dto;

namespace PM.Application.Roles
{
    public interface IRoleAppService:IAsyncCrudAppService<RoleDto,int,PagedResultRequestDto,CreateRoleDto,RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();

    }
}
