using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Veloly_Backend.Startup))]
namespace Veloly_Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
