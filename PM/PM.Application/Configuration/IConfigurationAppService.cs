using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using PM.Application.Configuration.Dto;

namespace PM.Application.Configuration
{
    public interface IConfigurationAppService:IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
