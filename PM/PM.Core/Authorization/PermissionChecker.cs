using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using PM.Core.Authorization.Roles;
using PM.Core.Users;

namespace PM.Core.Authorization
{
    public class PermissionChecker:PermissionChecker<Role,User>
    {
        public PermissionChecker(UserManager userManager) : base(userManager)
        {
        }
    }
}
