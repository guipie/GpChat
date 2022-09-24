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
using GuXin.Entity.DomainModels;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 内容收藏
    /// </summary>
    public partial class ContentsController : AppApiBaseController
    {
        /// <summary>
        /// 获取收藏内容
        /// </summary> 
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Collect")]
        public JsonResult GetCollect([FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(_contentService.CollectContents((DateTime)searchDate));
        }
        /// <summary>
        /// 新增收藏内容
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <returns></returns>
        [HttpPost("Collect/{contentGuid}")]
        public JsonResult AddCollect([FromRoute] Guid contentGuid)
        {
            return Json(_content_CollectService.PostCollect(contentGuid));
        }
    }
}
