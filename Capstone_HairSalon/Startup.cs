using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Capstone_HairSalon.Startup))]
namespace Capstone_HairSalon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
