/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹App_UpgradeController编写
 */
using Microsoft.AspNetCore.Mvc;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AttributeManager;
using GuXin.System.IServices;
namespace GuXin.System.Controllers
{
    [Route("api/App_Upgrade")]
    [PermissionTable(Name = "App_Upgrade")]
    public partial class App_UpgradeController : ApiBaseController<IApp_UpgradeService>
    {
        public App_UpgradeController(IApp_UpgradeService service)
        : base(service)
        {


        }
    }
}

