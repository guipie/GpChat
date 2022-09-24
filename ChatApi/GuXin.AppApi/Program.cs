using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GuXin.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.ConfigureKestrel(serverOptions =>
                       {
                           serverOptions.Limits.MaxRequestBodySize = 10485760;
                           // Set properties and call methods on options
                       });
                       //webBuilder.UseKestrel().UseUrls("http://*:3002");
                       webBuilder.UseIIS();
                       webBuilder.UseStartup<Startup>();
                   }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
