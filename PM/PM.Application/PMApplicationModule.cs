using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Modules;
using PM.Core;

namespace PM.Application
{
    [DependsOn(typeof(PMCoreModule),typeof(AbpAutoMapperModule))]
    public class PMApplicationModule:AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //TODO: 


        }
    }
}
