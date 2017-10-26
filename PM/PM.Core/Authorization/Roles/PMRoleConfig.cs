using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace PM.Core.Authorization.Roles
{
    public class PMRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //静态主机角色
            roleManagementConfig.StaticRoles.Add(new StaticRoleDefinition(
                StaticRoleNames.Host.Admin
                , MultiTenancySides.Host));

            //静态租户角色
            roleManagementConfig.StaticRoles.Add(new StaticRoleDefinition(
                StaticRoleNames.Tenants.Admin,
                MultiTenancySides.Tenant));
        }
    }
}
