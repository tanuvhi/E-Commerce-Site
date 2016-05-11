using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UltraShopBd.WebUI.Startup))]
namespace UltraShopBd.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
