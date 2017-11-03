using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.MultiTenancy;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using NSubstitute;
using PM.Application;
using PM.EntityFramework;

namespace PM.Test
{
    [DependsOn(
           typeof(PMDataModule),
           typeof(PMApplicationModule),
           typeof(AbpTestBaseModule)
       )]
    public class PMTestModule:AbpModule
    {
        public override void PreInitialize()
        {
            //使用数据库进行语言管理
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //注册伪服务

            IocManager.IocContainer.Register(
                Component.For<IAbpZeroDbMigrator>()
                    .UsingFactoryMethod(() => Substitute.For<IAbpZeroDbMigrator>())
                    .LifestyleSingleton()
                );
        }
    }
}
