using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM.Core.MultiTenant;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework.Migrations.SeedData
{
    /// <summary>
    /// 默认租户创建者
    /// </summary>
    public class DefaultTenantCreator
    {
        private readonly PMDBContext _context;

        public DefaultTenantCreator(PMDBContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //默认租户
            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant()
                {
                    TenancyName = Tenant.DefaultTenantName,
                    Name = Tenant.DefaultTenantName
                });
                _context.SaveChanges();
            }

        }

    }
}
