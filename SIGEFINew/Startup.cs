using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIGEFINew.Startup))]
namespace SIGEFINew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
