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
using GuXin.ImCore.Models;
using GuXin.Core.ManageUser;
using GuXin.Core.Utilities;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 用户通知
    /// </summary>
    [Route("Notice")]
    public class NoticesController : AppApiBaseController 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_Sys_NoticeService _userNoticeService;
        //private readonly IUser_Sys_Notice_SettingService _userNoticeSettingService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="user_Sys_NoticeService"></param>
        [ActivatorUtilitiesConstructor]
        public NoticesController(
            IHttpContextAccessor httpContextAccessor,
            IUser_Sys_NoticeService user_Sys_NoticeService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _userNoticeService = user_Sys_NoticeService;
            //_userNoticeSettingService = user_Sys_Notice_SettingService;
        }
        #region 系统通知
        [HttpGet]
        public JsonResult GetNotices([FromQuery] DateTime? searchDate)
        {
            return OkNormalData(_userNoticeService.Notices(searchDate ?? DateTime.Now));
        }

        /// <summary>
        /// 系统通知
        /// </summary>
        /// <returns></returns>
        [HttpGet("Sys")]
        public JsonResult SysNotices()
        {
            var result = WebResponseNormal.Instance.OK(ImHelper.GetUnreadNotice(UserContext.Current.UserId, ImCore.ChatType.Notice));
            return JsonNormal(result);
        }
        [HttpGet]
        [Route("Settings")]
        public JsonResult GetUserNoticeSettings()
        {
            return OkNormalData(_userNoticeService.Get());
        }
        [HttpPut]
        [Route("Settings/Top/{top}")]
        public WebResponseContent SetTop([FromRoute] bool top)
        {
            return WebResponseContent.Instance.Info(_userNoticeService.NoticeSetting(top, null));
        }
        [HttpPut("Settings/NotDisturb/{notDisturb}")]
        public WebResponseContent SetNotDisturb([FromRoute] bool notDisturb)
        {
            return WebResponseContent.Instance.Info(_userNoticeService.NoticeSetting(null, notDisturb));
        }

        #endregion 互动设置
        /// <summary>
        /// 互动通知
        /// </summary>
        [HttpGet("Interact")]
        public JsonResult InteractNotices()
        {
            var result = WebResponseNormal.Instance.OK(ImHelper.GetUnreadNotice(UserContext.Current.UserId, ImCore.ChatType.Comment));
            return JsonNormal(result);
        }
        /// <summary>
        /// 动态通知
        /// </summary>
        /// <returns></returns>
        [HttpGet("Follow")]
        public JsonResult FollowNotices()
        {
            var result = WebResponseNormal.Instance.OK(ImHelper.GetUnreadNotice(UserContext.Current.UserId, ImCore.ChatType.Follow));
            return JsonNormal(result);
        }
        /// <summary>
        /// 好友请求通知
        /// </summary>
        /// <returns></returns>
        [HttpGet("FriendRequest")]
        public JsonResult FriendRequestNotices()
        {
            var result = WebResponseNormal.Instance.OK(ImHelper.GetUnreadNotice(UserContext.Current.UserId, ImCore.ChatType.FriendRequest));
            return JsonNormal(result);
        }
        /// <summary>
        /// 好友接受通知
        /// </summary>
        /// <returns></returns>
        [HttpGet("FriendAccept")]
        public JsonResult FriendAcceptNotices()
        {
            var result = WebResponseNormal.Instance.OK(ImHelper.GetUnreadNotice(UserContext.Current.UserId, ImCore.ChatType.FriendAccept));
            return JsonNormal(result);
        }
    }
}
