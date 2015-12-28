using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToothCrystal.Startup))]
namespace ToothCrystal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
