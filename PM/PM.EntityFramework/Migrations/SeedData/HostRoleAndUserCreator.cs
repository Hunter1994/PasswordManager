using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Microsoft.AspNet.Identity;
using PM.Core.Authorization;
using PM.Core.Authorization.Roles;
using PM.Core.Users;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework.Migrations.SeedData
{
    /// <summary>
    /// 主机admin创建者
    /// </summary>
    public class HostRoleAndUserCreator
    {
        private readonly PMDBContext _context;

        public HostRoleAndUserCreator(PMDBContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //主机角色
            var adminRoleForHost= _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role()
                {
                    Name = StaticRoleNames.Host.Admin,
                    DisplayName = StaticRoleNames.Host.Admin,
                    IsStatic = true
                });
                _context.SaveChanges();

                //授予所有租户权限
                var permisstions = PermissionFinder.GetAllPermissions(new PMProjectNameAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();
                foreach (var permisstion in permisstions)
                {
                    _context.Permissions.Add(new RolePermissionSetting()
                    {
                        Name = permisstion.Name,
                        IsGranted = true,
                        RoleId = adminRoleForHost.Id
                    });
                }
                _context.SaveChanges();
            }

            //主机admin
            var adminUserForHost =
                _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(new User()
                {
                    UserName = User.AdminUserName,
                    Name = "System",
                    Surname = "Administrator",
                    EmailAddress = "qq962410314@163.com",
                    IsEmailConfirmed = true,
                    Password = new PasswordHasher().HashPassword(User.DefaultPassword)
                });

                _context.SaveChanges();
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();
            }


        }

    }
}
