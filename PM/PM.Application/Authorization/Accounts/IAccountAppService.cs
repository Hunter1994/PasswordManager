using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using PM.Application.Authorization.Accounts.Dto;

namespace PM.Application.Authorization.Accounts
{
    public interface  IAccountAppService:IApplicationService
    {
        /// <summary>
        /// 租户可用是否可用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IsTenantAvaliableOutput> IsTenantAvaliable(IsTenantAvaliableInput input);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RegisterOutput> Register(RegisterInput input);
    }
}
