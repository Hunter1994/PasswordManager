using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using PM.Core.MultiTenant;

namespace PM.Application.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class EditTenantDto
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
         public string TenancyName { get; set; }


    }
}
