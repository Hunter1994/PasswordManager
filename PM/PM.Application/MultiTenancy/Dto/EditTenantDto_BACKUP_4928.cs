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
    [AutoMapTo(typeof (Tenant))]
    public class EditTenantDto
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }
<<<<<<< HEAD

        public bool IsActive { get; set; }
=======
>>>>>>> 75f0b478152396ef7b98500e7af4e14492a2e193

        public bool IsActive { get; set; }
    }
}
