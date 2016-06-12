using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AuctionHouseService.Startup))]

namespace AuctionHouseService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}