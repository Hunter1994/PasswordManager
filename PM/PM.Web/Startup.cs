using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PM.Web.Startup))]
namespace PM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
