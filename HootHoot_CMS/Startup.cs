using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HootHoot_CMS.Startup))]
namespace HootHoot_CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
