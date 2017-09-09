using System.Web.Http;
using System.Web.Http.Cors;
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

        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute(origins: "http://veloly-backend20170909063305.azurewebsites.net", headers: "*", methods: "*");
            config.EnableCors(cors);
        }
    }
}
