using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Grademeter_Assignment1.Startup))]
namespace Grademeter_Assignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
