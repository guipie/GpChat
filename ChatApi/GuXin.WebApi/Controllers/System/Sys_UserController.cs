
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AttributeManager;
using GuXin.Entity.DomainModels;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    [Route("api/Sys_User")]
    [PermissionTable(Name = "Sys_User")]
    public partial class Sys_UserController : ApiBaseController<ISys_UserService>
    {
        public Sys_UserController(ISys_UserService service)
        : base("System", "System", "Sys_User", service)
        {
            //, IMemoryCache cache
        }
    }
}
