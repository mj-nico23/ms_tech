using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ms_tech.Startup))]
namespace ms_tech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
