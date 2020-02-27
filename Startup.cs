using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicroFinBank.Startup))]
namespace MicroFinBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
