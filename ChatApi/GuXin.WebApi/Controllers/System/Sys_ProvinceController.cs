/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹Sys_ProvinceController编写
 */
using Microsoft.AspNetCore.Mvc;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AttributeManager;
using GuXin.System.IServices;
namespace GuXin.System.Controllers
{
    [Route("api/Sys_Province")]
    [PermissionTable(Name = "Sys_Province")]
    public partial class Sys_ProvinceController : ApiBaseController<ISys_ProvinceService>
    {
        public Sys_ProvinceController(ISys_ProvinceService service)
        : base(service)
        {
        }
    }
}

