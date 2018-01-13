using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jan2018DemoWebSite.Startup))]
namespace Jan2018DemoWebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
