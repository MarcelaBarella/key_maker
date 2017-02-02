using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kramer.Startup))]
namespace Kramer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
