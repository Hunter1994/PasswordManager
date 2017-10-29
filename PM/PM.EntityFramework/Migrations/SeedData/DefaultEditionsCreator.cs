using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Editions;
using PM.Core.Editions;
using PM.EntityFramework.EntityFramework;

namespace PM.EntityFramework.Migrations.SeedData
{

    /// <summary>
    /// 默认版本创建者
    /// </summary>
    public class DefaultEditionsCreator
    {
        private readonly PMDBContext _context;

        public DefaultEditionsCreator(PMDBContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEdtions();
        }

        private void CreateEdtions()
        {
            var defaultEdtion = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdtion == null)
            {
                defaultEdtion = new Edition()
                {
                    Name = EditionManager.DefaultEditionName,
                    DisplayName = EditionManager.DefaultEditionName
                };
                _context.Editions.Add(defaultEdtion);
                _context.SaveChanges();

                //TODO：如果需要，可以在标准版中添加所需的功能！
            }
        }

    }
}
