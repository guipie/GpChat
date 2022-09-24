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
using GuXin.Core.Utilities;

namespace GuXin.App.Controllers
{
    /// <summary>
    ///内容 赞/新赞
    /// </summary>
    public partial class ContentsController : AppApiBaseController
    {
        /// <summary>
        /// 获取内容点赞用户
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Praise/{contentGuid}")]
        public JsonResult GetPraiseUsers([FromRoute] Guid contentGuid, [FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(_content_PraiseService.GetPraiseUsers(contentGuid, (DateTime)searchDate));
        }
        /// <summary>
        /// 获取朋友圈点赞用户
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Praise/Friend/{contentGuid}")]
        public JsonResult GetPraiseFriendUsers([FromRoute] Guid contentGuid, [FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(_content_PraiseService.GetFriendPraiseUsers(contentGuid, (DateTime)searchDate));
        }
        /// <summary>
        /// 点赞公共内容
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <returns></returns>
        [HttpPost("Praise/{contentGuid}")]
        public WebResponseContent AddPraise([FromRoute] Guid contentGuid)
        {
            return _content_PraiseService.PostPraise(contentGuid);
        } 
        /// <summary>
        /// 点赞朋友圈内容
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <returns></returns>
        [HttpPost("Praise/Friend/{contentGuid}")]
        public WebResponseContent AddFriendPraise([FromRoute] Guid contentGuid)
        {
            return _content_PraiseService.PostFriendPraise(contentGuid);
        }
    }
}
