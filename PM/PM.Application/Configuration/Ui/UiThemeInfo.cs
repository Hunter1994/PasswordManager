using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Application.Configuration.Ui
{
    public class UiThemeInfo
    {
        public string Name { get; set; }
        public string CssClass { get; set; }

        public UiThemeInfo(string name,string cssClass)
        {
            this.Name = name;
            this.CssClass = cssClass;
        }
    }
}
