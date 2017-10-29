using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Roles;
using PM.Core.Users;

namespace PM.Core.Authorization.Roles
{
    public class Role : AbpRole<User>
    {
        public const int MaxDescriptionLength = 5000;

        public Role()
        {
        }

        public Role(int? tenantId, string displayName) : base(tenantId, displayName)
        {
        }

        public Role(int? tenantId, string name, string displayName) : base(tenantId, name, displayName)
        {
        }
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }


    }
}
