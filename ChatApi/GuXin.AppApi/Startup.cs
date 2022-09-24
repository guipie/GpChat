using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuXin.Core.Configuration;
using GuXin.Core.Extensions;
using GuXin.Core.Filters;
using GuXin.Core.Middleware;
using GuXin.Core.ObjectActionValidator;
using Newtonsoft.Json;

namespace GuXin.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IServiceCollection Services { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //初始化模型验证配置
            services.UseMethodsModelParameters().UseMethodsGeneralParameters();
            services.AddSingleton<IObjectModelValidator>(new NullObjectModelValidator());
            Services = services;

            services.AddSession();
            services.AddMemoryCache();

            services.AddHttpContextAccessor();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AppApiAuthorizeFilter));
                options.Filters.Add(typeof(ActionExecuteFilter));
                //options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers(opt =>
            {
                opt.UseCentralRoutePrefix(new RouteAttribute("AppApi/V1"));
            })
             .AddNewtonsoftJson(op =>
             {
                 op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                 op.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
             });
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,//保存token,后台验证token是否生效(重要)
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = AppSetting.Secret.Audience,//Audience
                    ValidIssuer = AppSetting.Secret.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Secret.JWT))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.Clear();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 401;
                        context.Response.WriteAsync(new { message = "授权未通过", status = false, code = 401 }.Serialize());
                        return Task.CompletedTask;
                    }
                };
            });

            //必须appsettings.json中配置
            string corsUrls = Configuration["CorsUrls"];
            if (string.IsNullOrEmpty(corsUrls))
            {
                throw new Exception("请配置跨请求的前端Url");
            }
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(corsUrls.Split(","))
                        //添加预检请求过期时间
                         .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                        .AllowCredentials()
                        .AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "故新App接口文档", Version = "v1" });
                var security = new Dictionary<string, IEnumerable<string>>
                { { AppSetting.Secret.Issuer, new string[] { } }};
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT授权token前面需要加上字段Bearer与一个空格,如Bearer token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "GuXin.Entity.XML"), true);
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "GuXin.AppApi.XML"), true);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            })
             .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
                options.ClientErrorMapping[404].Link =
                    "https://*/404";
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            Services.AddModule(builder, Configuration);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseMiddleware<ExceptionHandlerMiddleWare>();
            app.Use(HttpRequestMiddleware.Context);

            app.UseStaticFiles().UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Upload")),
                //配置访问虚拟目录时文件夹别名
                RequestPath = "/Upload",
                OnPrepareResponse = (Microsoft.AspNetCore.StaticFiles.StaticFileResponseContext staticFile) =>
                {
                    //可以在此处读取请求的信息进行权限认证
                    //  staticFile.File
                    //  staticFile.Context.Response.StatusCode;
                }
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "故新APP接口-v1");
                   c.DocExpansion(DocExpansion.None); //->修改界面打开时自动折叠
               }
                );
            }
            //配置HttpContext
            app.UseStaticHttpContext();

            app.UseRouting();

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=ApiHome}/{action=Index}/{id?}");
            });



            #region imserver注册
            Console.WriteLine($"{(env.EnvironmentName)}环境启动,WebApi地址:[{configuration["Urls"]}],ImServer地址:[{configuration["ImServerUrls"]}]");

            ImHelper.Initialization(new ImClientOptions
            {
                Redis = new FreeRedis.RedisClient(AppSetting.RedisConnectionString),
                Servers = AppSetting.ImServerUrls
            });

            ImHelper.Instance.OnSend += (s, e) => Console.WriteLine($"ImClient.SendMessage(server={e.Server},data={JsonConvert.SerializeObject(e.Message)})");

            ImHelper.EventBus(
                t =>
                {
                    Console.WriteLine(t.clientId + DateTime.Now.ToString() + "上线了");
                    //var onlineUids = ImHelper.GetClientListByOnline();
                    //ImHelper.SendMessage(t.clientId, onlineUids, $"用户{t.clientId}上线了");
                },
                t =>
                {
                    Console.WriteLine(t.clientId + DateTime.Now.ToString() + "下线了");
                });
            #endregion  
        }
    }
}
