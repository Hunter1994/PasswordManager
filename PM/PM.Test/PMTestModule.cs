using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.TestBase;
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
    }
}
