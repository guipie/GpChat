/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹Sys_AgreementController编写
 */
using Microsoft.AspNetCore.Mvc;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AttributeManager;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    [Route("api/Sys_Agreement")]
    [PermissionTable(Name = "Sys_Agreement")]
    public partial class Sys_AgreementController : ApiBaseController<ISys_AgreementService>
    {
        public Sys_AgreementController(ISys_AgreementService service)
        : base(service)
        {
        }
    }
}

