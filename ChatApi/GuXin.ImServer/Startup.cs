using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;

namespace imServer
{

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
        {
            //app.Run(async c =>
            //{
            //    await c.Response.WriteAsync(env.EnvironmentName);
            //    await c.Response.WriteAsync("Server:" + config["ImServerOption:Server"]);
            //    await c.Response.WriteAsync("Servers:" + config["ImServerOption:Servers"]);
            //});
            Console.WriteLine($"【{env.EnvironmentName}】ImServer启动[{config["ImServerOption:Server"]}]");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Console.OutputEncoding = Encoding.GetEncoding("GB2312");
            //Console.InputEncoding = Encoding.GetEncoding("GB2312");

            app.UseDeveloperExceptionPage();

            app.UseImServer(new ImServerOptions
            {
                Redis = new FreeRedis.RedisClient(Configuration["ImServerOption:RedisClient"]),
                Servers = Configuration["ImServerOption:Servers"].Split(";"),
                Server = Configuration["ImServerOption:Server"]
            });
            app.Build();
        }
    }
}
