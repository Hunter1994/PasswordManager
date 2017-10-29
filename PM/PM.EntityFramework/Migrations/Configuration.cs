using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using PM.EntityFramework.Migrations.SeedData;

namespace PM.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<PM.EntityFramework.EntityFramework.PMDBContext>,IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PM";
        }

        //Seed() 方法会在你每次你执行 Update-Database 指令时被呼叫一次
        protected override void Seed(PM.EntityFramework.EntityFramework.PMDBContext context)
        {
            //禁用所有过滤
            context.DisableAllFilters();
            if (Tenant == null)
            {
                //主机种子
                new InitialHostDbBuilder(context).Create();

                //默认租户种子
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //您可以为租户数据库添加种子并使用租户属性...
            }
            context.SaveChanges();
        }

    }
}
