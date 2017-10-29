using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework.Migrations.SeedData
{
    /// <summary>
    /// 初始化主机数据库提供者
    /// </summary>
    public class InitialHostDbBuilder
    {
        private readonly PMDBContext _context;

        public InitialHostDbBuilder(PMDBContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //禁用所有过滤器
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

        }

    }
}
