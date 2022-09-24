using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GuXin.Core.Controllers.Basic;
using GuXin.Core.Enums;
using GuXin.Core.Filters;
using GuXin.Entity.AttributeManager;
using GuXin.Entity.DomainModels;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    [Route("api/Sys_Role")]
    [PermissionTable(Name = "Sys_Role")]
    public partial class Sys_RoleController : ApiBaseController<ISys_RoleService>
    {
        public Sys_RoleController(ISys_RoleService service)
        : base("System", "System", "Sys_Role", service)
        {

        }
    }
}


