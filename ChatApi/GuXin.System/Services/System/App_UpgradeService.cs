/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下App_UpgradeService与IApp_UpgradeService中编写
 */
using GuXin.System.IRepositories;
using GuXin.System.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Services
{
    public partial class App_UpgradeService : ServiceBase<App_Upgrade, IApp_UpgradeRepository>
    , IApp_UpgradeService, IDependency
    {
    public App_UpgradeService(IApp_UpgradeRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static IApp_UpgradeService Instance
    {
      get { return AutofacContainerModule.GetService<IApp_UpgradeService>(); } }
    }
 }
