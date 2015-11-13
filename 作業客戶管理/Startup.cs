using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(作業客戶管理.Startup))]
namespace 作業客戶管理
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
