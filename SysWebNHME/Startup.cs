using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SysWebNHME.Startup))]
namespace SysWebNHME
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
