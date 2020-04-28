using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BellVotingSystem.WEB.Areas.Identity.IdentityHostingStartup))]
namespace BellVotingSystem.WEB.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}