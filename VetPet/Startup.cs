using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VetPet.Startup))]
namespace VetPet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
