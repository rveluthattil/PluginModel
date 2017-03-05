using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PluginModel.Startup))]
namespace PluginModel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
