using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Parqueadero.Startup))]
namespace Parqueadero
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
