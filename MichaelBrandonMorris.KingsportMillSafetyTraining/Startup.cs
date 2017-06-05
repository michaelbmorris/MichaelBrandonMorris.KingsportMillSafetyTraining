using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MichaelBrandonMorris.KingsportMillSafetyTraining.Startup))]
namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
