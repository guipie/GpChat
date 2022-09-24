using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace imServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .UseWindowsService() //++支持Windows服务
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.Sources.Clear();
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true);
                    });
                    //webBuilder.UseKestrel().UseUrls("http://::3003/");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
