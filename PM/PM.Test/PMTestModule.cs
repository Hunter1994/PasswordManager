using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using PM.Application;
using PM.EntityFramework;

namespace PM.Test
{
    [DependsOn(
           typeof(PMDataModule),
           typeof(PMApplicationModule)
       )]
    public class PMTestModule:AbpModule
    {
    }
}
