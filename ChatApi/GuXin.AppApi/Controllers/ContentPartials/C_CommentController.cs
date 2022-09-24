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
    /// 内容评论
    /// </summary>
    public partial class ContentsController : AppApiBaseController
    {
        /// <summary>
        /// 获取内容评论
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Comment/{contentGuid}")]
        public JsonResult GetComments([FromRoute] Guid contentGuid, [FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(_content_CommentService.GetComments(contentGuid, (DateTime)searchDate));
        }
        /// <summary>
        /// 点赞评论
        /// </summary>
        /// <param name="commentsId"></param>
        /// <returns></returns>
        [HttpPut("Comment/Praise/{commentsId}")]
        public JsonResult PraiseComments([FromRoute] int commentsId)
        {
            return OkData(_content_CommentService.PraiseComment(commentsId));
        }
        /// <summary>
        /// 获取朋友圈评论
        /// </summary>
        /// <param name="contentGuid"></param> 
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Comment/Friend/{contentGuid}")]
        public JsonResult GetFriendComments([FromRoute] Guid contentGuid, [FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(_content_CommentService.GetFriendComments(contentGuid, (DateTime)searchDate));
        }
        /// <summary>
        /// 新增内容评论
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <param name="comments"></param>
        /// <param name="parentId">回复内容</param>
        /// <returns></returns>
        [HttpPost("Comment/{contentGuid}")]
        public JsonResult AddComment([FromRoute] Guid contentGuid, [FromQuery] string comments, [FromQuery] int parentId)
        {
            var result = _content_CommentService.PostComment(contentGuid, comments, parentId);
            return OkData(result);
        }
        [HttpPut("Comment/{commentsId}")]
        public JsonResult PutComment([FromRoute] int commentsId, [FromQuery] string comments)
        {
            var result = _content_CommentService.PutComment(commentsId, comments); 
            return OkData(result);
        }
        [HttpDelete("Comment/{commentsId}")]
        public JsonResult DelComment([FromRoute] int commentsId)
        {
            var result = _content_CommentService.DelComment(commentsId);
            return OkData(result);
        }
        /// <summary>
        /// 新增朋友圈评论
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <param name="comments"></param>
        /// <param name="parentId">回复内容</param>
        /// <returns></returns>
        [HttpPost("Comment/Friend/{contentGuid}")]
        public JsonResult AddFriendComment([FromRoute] Guid contentGuid, [FromQuery] string comments, [FromQuery] int parentId)
        {
            return OkData(_content_CommentService.PostFriendComment(contentGuid, comments, parentId));
        }
    }
}