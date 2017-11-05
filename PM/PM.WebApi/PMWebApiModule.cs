using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using PM.Application;

namespace PM.WebApi
{
    [DependsOn(typeof(AbpWebApiModule),typeof(PMApplicationModule))]
    public class PMWebApiModule:AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi()
                .DynamicApiControllerBuilder.ForAll<IApplicationService>(typeof (PMApplicationModule).Assembly, "app")
                .Build();

            //表示通过OWIN中间件进行身份验证的身份验证筛选器（认证类型为Bearer）
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

        }

    }
}
