using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM.Core.MultiTenant;

namespace PM.Application.Authorization.Accounts.Dto
{
    public class IsTenantAvaliableInput
    {
        [Required]
        [MaxLength(Tenant.MaxTenancyNameLength)]
        public string TenantName { get; set; }
    }

    public class IsTenantAvaliableOutput
    {
        public TenantAvaliablityState State { get; set; }

        public int? TenantId { get; set; }

        public IsTenantAvaliableOutput()
        {

        }

        public IsTenantAvaliableOutput(TenantAvaliablityState state, int? tenantId = null)
        {
            State = state;
            TenantId = tenantId;
        }

    }

}
