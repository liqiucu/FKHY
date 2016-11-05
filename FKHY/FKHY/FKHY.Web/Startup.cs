using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FKHY.Web.Startup))]
namespace FKHY.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
