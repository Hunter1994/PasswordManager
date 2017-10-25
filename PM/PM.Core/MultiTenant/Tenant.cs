using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MultiTenancy;
using PM.Core.Users;

namespace PM.Core.MultiTenant
{
    public class Tenant:AbpTenant<User>
    {

    }
}
