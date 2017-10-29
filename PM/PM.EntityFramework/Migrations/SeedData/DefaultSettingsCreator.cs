using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework.Migrations.SeedData
{
    /// <summary>
    /// 默认设置创建者
    /// </summary>
    public class DefaultSettingsCreator
    {
        private readonly PMDBContext _context;

        public DefaultSettingsCreator(PMDBContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //邮箱
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "qq962410314@163.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "qq962410314@163.com");

            //语言
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "zh-CN");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }
            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }


    }
}
