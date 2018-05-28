using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AniHelp.WEB.Startup))]
namespace AniHelp.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
