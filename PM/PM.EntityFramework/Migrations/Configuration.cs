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

        //Seed() ����������ÿ����ִ�� Update-Database ָ��ʱ������һ��
        protected override void Seed(PM.EntityFramework.EntityFramework.PMDBContext context)
        {
            //�������й���
            context.DisableAllFilters();
            if (Tenant == null)
            {
                //��������
                new InitialHostDbBuilder(context).Create();

                //Ĭ���⻧����
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //������Ϊ�⻧���ݿ�������Ӳ�ʹ���⻧����...
            }
            context.SaveChanges();
        }

    }
}
