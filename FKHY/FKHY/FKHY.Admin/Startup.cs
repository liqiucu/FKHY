using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FKHY.Admin.Startup))]
namespace FKHY.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
