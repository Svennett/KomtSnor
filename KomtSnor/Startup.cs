using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KomtSnor.Startup))]
namespace KomtSnor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
