using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Distribution.Web.En.Startup))]
namespace Distribution.Web.En
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
