using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NationAssists.Startup))]
namespace NationAssists
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
