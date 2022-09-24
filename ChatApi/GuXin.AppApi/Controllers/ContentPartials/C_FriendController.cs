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
    /// <summary>
    /// 朋友圈相关内容
    /// </summary> 
    public partial class ContentsController : AppApiBaseController
    {

        /// <summary>
        /// 朋友圈内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Friend")]
        public async Task<JsonResult> GetFriendContentsAsync([FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(await _contentService.FriendContentsAsync((DateTime)searchDate));
        }
        [HttpGet("Friend/Photos")]
        public JsonResult GetFriendPhotos([FromQuery] DateTime? searchDate)
        {
            return OkData(_contentService.FriendPics(searchDate ?? DateTime.Now));
        }
        /// <summary>
        /// 某个用户朋友圈内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Friend/{userId}")]
        public async Task<JsonResult> GetFriendUserContentsAsync([FromQuery] DateTime? searchDate, [FromRoute] int userId)
        {
            searchDate ??= DateTime.Now;
            return OkData(await _contentService.FriendContentsAsync((DateTime)searchDate, userId));
        }
    }
}
