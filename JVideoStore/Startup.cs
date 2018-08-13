using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JVideoStore.Startup))]
namespace JVideoStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
