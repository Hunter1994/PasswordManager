using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Zero.EntityFramework;
using PM.Core.Authorization.Roles;
using PM.Core.MultiTenant;
using PM.Core.Users;

namespace PM.EntityFramework.EntityFramework
{
    public class PMDBContext:AbpZeroDbContext<Tenant,Role,User>
    {
        /// <summary>
        /// 将“默认”设置为基类可帮助我们在Package Manager Console上执行迁移命令时使用。
        /// 但是，在运行EF的Migrate.exe时可能会导致问题。 如果要在命令行上应用迁移，请不要将连接字符串名称传递给基类。 ABP工作方式。
        /// </summary>
        public PMDBContext() : base("Default")
        {
        }

        /// <summary>
        /// ABP使用此构造函数传递PMDBContext.PreInitialize中定义的连接字符串。
        /// 注意，实际上你不会直接创建一个PMDBContext的实例，因为ABP自动处理它。
        /// </summary>
        /// <param name="nameOrnameOrConnectionString"></param>
        public PMDBContext(string nameOrnameOrConnectionString) : base(nameOrnameOrConnectionString)
        {
        }

        /// <summary>
        /// 这个构造函数用于测试
        /// </summary>
        /// <param name="existingConnection"></param>
        public PMDBContext(DbConnection existingConnection) : base(existingConnection,false)
        {
        }

        public PMDBContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

    }
}
