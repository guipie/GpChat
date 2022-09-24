using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GuXin.Core.Controllers.Basic;
using GuXin.Core.Enums;
using GuXin.Core.Filters;
using GuXin.Entity.DomainModels;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    [Route("api/menu")]
    [ApiController, JWTAuthorize()]
    public partial class Sys_MenuController : ApiBaseController<ISys_MenuService>
    {
        private ISys_MenuService _service { get; set; }
        public Sys_MenuController(ISys_MenuService service) :
            base("System", "System", "Sys_Menu", service)
        {
            _service = service;
        } 
    }
}
