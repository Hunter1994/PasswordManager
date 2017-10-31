using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PM.Application.MultiTenancy.Dto;
using PM.Core.MultiTenant;

namespace PM.Application.MultiTenancy
{
    public interface ITenantAppService:IAsyncCrudAppService<TenantDto, Int32,PagedResultRequestDto,CreateTenantDto,TenantDto>
    {

    }
}
