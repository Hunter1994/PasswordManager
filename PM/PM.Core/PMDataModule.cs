using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.Zero;

namespace PM.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class PMDataModule:AbpModule
    {
        public override void PreInitialize()
        {
            //开启多租户
            Configuration.MultiTenancy.IsEnabled = true;


        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
