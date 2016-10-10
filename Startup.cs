using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TAJdamaProject.Startup))]
namespace TAJdamaProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
