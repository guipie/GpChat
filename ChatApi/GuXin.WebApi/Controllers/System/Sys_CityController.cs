/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹Sys_CityController编写
 */
using Microsoft.AspNetCore.Mvc;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AttributeManager;
using GuXin.System.IServices;
namespace GuXin.System.Controllers
{
    [Route("api/Sys_City")]
    [PermissionTable(Name = "Sys_City")]
    public partial class Sys_CityController : ApiBaseController<ISys_CityService>
    {
        public Sys_CityController(ISys_CityService service)
        : base(service)
        {
        }
    }
}

