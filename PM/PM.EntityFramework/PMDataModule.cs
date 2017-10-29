using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using PM.Core;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework
{
    [DependsOn(typeof(PMCoreModule),typeof(AbpZeroEntityFrameworkModule))]
    public class PMDataModule:AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PMDBContext>());
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
