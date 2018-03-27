using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WaterMarkImage.Startup))]
namespace WaterMarkImage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
