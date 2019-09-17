using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aplicada.Startup))]
namespace Aplicada
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
