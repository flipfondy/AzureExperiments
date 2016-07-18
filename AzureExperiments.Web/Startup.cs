using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureExperiments.Web.Startup))]
namespace AzureExperiments.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
