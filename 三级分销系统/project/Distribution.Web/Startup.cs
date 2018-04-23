using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Distribution.Web.Startup))]
namespace Distribution.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
