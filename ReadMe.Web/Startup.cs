using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReadMe.Web.Startup))]
namespace ReadMe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
