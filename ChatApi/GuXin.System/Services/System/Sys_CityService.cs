/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Sys_CityService与ISys_CityService中编写
 */
using GuXin.System.IRepositories;
using GuXin.System.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Services
{
    public partial class Sys_CityService : ServiceBase<Sys_City, ISys_CityRepository>
    , ISys_CityService, IDependency
    {
    public Sys_CityService(ISys_CityRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static ISys_CityService Instance
    {
      get { return AutofacContainerModule.GetService<ISys_CityService>(); } }
    }
 }
