using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using PM.Core.Authorization;
using PM.Core.Authorization.Roles;
using PM.Core.Users;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework.Migrations.SeedData
{
    /// <summary>
    /// 租户admin创建者
    /// </summary>
    public class TenantRoleAndUserBuilder
    {
        private readonly PMDBContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(PMDBContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {

            //租户角色
            var adminRole =
                _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole==null)
            {
                adminRole =
                    _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin)
                    {
                        IsStatic = true
                    });
                _context.SaveChanges();

                //授予管理员角色的所有权限
                var permisstions = PermissionFinder.GetAllPermissions(new PMProjectNameAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();
                foreach (var permisstion in permisstions)
                {
                    _context.Permissions.Add(new RolePermissionSetting()
                    {
                        TenantId = _tenantId,
                        Name = permisstion.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    });
                }
                _context.SaveChanges();
            }

            //租户admin
            var adminUser =
                _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == User.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "qq962410314@163.com", User.DefaultPassword);
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}
