using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PM.Core.MultiTenant;

namespace PM.Application.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto:EntityDto
    {
        public string TenancyName { get; set; }
        public string Name { get; set; }

    }
}
