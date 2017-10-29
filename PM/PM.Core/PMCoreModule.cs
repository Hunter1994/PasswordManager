using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using PM.Core.Authorization;
using PM.Core.Authorization.Roles;
using PM.Core.Configuration;
using PM.Core.MultiTenant;
using PM.Core.Users;

namespace PM.Core
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class PMCoreModule:AbpModule
    {
        public override void PreInitialize()
        {
            //如果当前用户未登录，请设置为true以启用保存审核日志。默认值：
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //声明实体类型
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof (Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof (Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof (User);

            //开启多租户
            Configuration.MultiTenancy.IsEnabled = PMProjectNameConsts.MultiTenantEnabled;

            //添加删除本地化源
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(PMProjectNameConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(Assembly.GetExecutingAssembly(),
                        "PM.Localization.Source")));

            //设置静态角色
            PMRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            //初始化权限
            Configuration.Authorization.Providers.Add<PMProjectNameAuthorizationProvider>();

            //初始化设置
            Configuration.Settings.Providers.Add<PMSettingProvider>();

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
