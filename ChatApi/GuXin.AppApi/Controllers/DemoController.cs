using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuXin.Core.Controllers.Basic;

namespace GuXin.App.Controllers
{
    [Route("Demo")]
    public class UserXinPraiseController : AppApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public UserXinPraiseController(
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("top"), AllowAnonymous]
        public JsonResult Get([FromQuery] int a)
        {
            return Json(a);
        }
    }
}
