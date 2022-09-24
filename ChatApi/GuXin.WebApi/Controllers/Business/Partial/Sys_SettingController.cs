/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("Sys_Setting",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.Core.Filters;
using GuXin.Entity.AppDto.Input;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{

    [ApiActionPermission(ActionRolePermission.SuperAdmin)]
    public partial class Sys_SettingController
    {
        private readonly ISys_SettingService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public Sys_SettingController(
            ISys_SettingService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("Recommend")]
        public JsonResult GetRecommend()
        {
            return Json(_service.GetReSettings());
        }
        [HttpPost("Recommend")]
        public JsonResult PostRecommend([FromBody] RecommendSettingDto dto)
        {
            return Json(_service.PostReSettings(dto));
        }

        [HttpGet("Top")]
        public JsonResult GetTopcommend()
        {
            return Json(_service.GetTopSettings());
        }
        [HttpPost("Top")]
        public JsonResult PostTopecommend([FromBody] TopSettingDto dto)
        {
            return Json(_service.PostTopSettings(dto));
        }
    }
}
