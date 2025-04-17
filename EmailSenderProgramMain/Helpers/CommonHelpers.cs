using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmailSenderProgramMain.Helpers
{
    class CommonHelpers
    {
        public static IConfiguration GetAppConfig(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).Build();
            return host.Services.GetRequiredService<IConfiguration>();

        }
    }
}
