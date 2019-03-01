using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(coreblog.Startup))]
namespace coreblog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
