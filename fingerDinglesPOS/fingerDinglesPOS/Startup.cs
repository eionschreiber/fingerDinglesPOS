using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fingerDinglesPOS.Startup))]
namespace fingerDinglesPOS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
