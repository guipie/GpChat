using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuXin.Business.IServices;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AppDto.Input;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 内容简介
    /// </summary>
    [Route("ContentIntro")]
    public class ContentIntroController : AppApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_IntroService _IntroService;
        [ActivatorUtilitiesConstructor]
        public ContentIntroController(
            IHttpContextAccessor httpContextAccessor,
            IContent_IntroService content_IntroService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _IntroService = content_IntroService;
        }
        /// <summary>
        /// 获取用户简介列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public JsonResult Get([FromRoute] int userId, [FromQuery] DateTime? searchDate)
        {
            return OkData(_IntroService.Get(userId, searchDate ?? DateTime.Now));
        }

        [HttpPost]
        public JsonResult Post([FromBody] List<ContentIntroInput> dtos)
        {
            return OkDataShow(_IntroService.Post(dtos), "发布成功");
        }
        [HttpPut]
        public JsonResult Put([FromBody] ContentIntroInput dto)
        {
            return OkDataShow(_IntroService.Put(dto), "编辑成功");
        }

        [HttpPut("Praise/{introGuid}")]
        public JsonResult Praise([FromRoute] Guid introGuid)
        {
            return Json(_IntroService.Praise(introGuid));
        }
        [HttpDelete("{introGuid}")]
        public JsonResult Del([FromRoute] Guid introGuid)
        {
            return Json(_IntroService.Del(introGuid));
        }
    }
}
