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
using GuXin.Core.Utilities;

namespace GuXin.App.Controllers
{
    public partial class FriendController : AppApiBaseController
    {
        #region 关注通知设置
        [HttpGet]
        [Route("Follow/Settings")]
        public JsonResult GetUserSettings()
        {
            return OkNormalData(_FollowService.GetCurrentSettings());
        }
        [HttpPut]
        [Route("Follow/Settings/Top/{top}")]
        public WebResponseContent SetTop([FromRoute] bool top)
        {
            return WebResponseContent.Instance.Info(_FollowService.IsTop(top));
        }
        [HttpPut("Follow/Settings/NotDisturb/{notDisturb}")]
        public WebResponseContent SetNotDisturb([FromRoute] bool notDisturb)
        {
            return WebResponseContent.Instance.Info(_FollowService.IsNotDisturb(notDisturb));
        }
        [HttpPut("Follow/Settings/Disable/{disable}")]
        public WebResponseContent SetDisable([FromRoute] bool disable)
        {
            return WebResponseContent.Instance.Info(_FollowService.IsDisable(disable));
        }
        #endregion

        [HttpPost("Follow/{userId:int}")]
        public JsonResult FollowOrUnFollow(int userId)
        {
            return Json(_FollowService.FollowOrUnFollow(userId));
        }
        [HttpGet]
        public JsonResult FollowUsers(DateTime? searchDate)
        {
            searchDate = searchDate == null ? DateTime.Now : searchDate;
            return Json(_FollowService.GetFollowUsers((DateTime)searchDate));
        }
    }
}
