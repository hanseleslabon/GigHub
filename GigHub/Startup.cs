using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GigHub.Startup))]
namespace GigHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
