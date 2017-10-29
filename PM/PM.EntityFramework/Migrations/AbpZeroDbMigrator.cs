using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework.Migrations
{
    /// <summary>
    /// 执行数据库迁移
    /// </summary>
    public class AbpZeroDbMigrator : AbpZeroDbMigrator<PMDBContext, Configuration>
    {
        public AbpZeroDbMigrator(IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver, IIocResolver iocResolver)
            : base(unitOfWorkManager, connectionStringResolver, iocResolver)
        {
        }
    }
}
