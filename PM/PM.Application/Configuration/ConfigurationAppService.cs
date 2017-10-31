using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using PM.Application.Configuration.Dto;
using PM.Core.Configuration;

namespace PM.Application.Configuration
{
    public class ConfigurationAppService : PMAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await
                SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), PMSettingNames.UiTheme,
                    input.Theme);
        }
    }
}
