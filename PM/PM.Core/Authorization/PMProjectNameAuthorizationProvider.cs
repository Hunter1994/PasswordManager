using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.MultiTenancy;

namespace PM.Core.Authorization
{
    public class PMProjectNameAuthorizationProvider: AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {

            context.CreatePermission(PermisstionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermisstionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermisstionNames.Pages_Tenants, L("Tenants"),multiTenancySides:MultiTenancySides.Host);

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PMProjectNameConsts.LocalizationSourceName);
        }

    }
}
