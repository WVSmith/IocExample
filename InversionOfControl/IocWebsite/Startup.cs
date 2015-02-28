using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IocWebsite.Startup))]
namespace IocWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
