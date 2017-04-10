using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CampusSecurity.Startup))]
namespace CampusSecurity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
