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
using GuXin.Core.Utilities;
using GuXin.Entity.AppEnum;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 好友相关接口
    /// </summary>
    [Route("Friend")]
    public partial class FriendController : AppApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_FriendService _friendService;
        private readonly IUser_FollowService _FollowService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="friendService"></param>
        [ActivatorUtilitiesConstructor]
        public FriendController(
            IHttpContextAccessor httpContextAccessor, IUser_FriendService friendService, IUser_FollowService user_FollowService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _friendService = friendService;
            _FollowService = user_FollowService;
        }
        /// <summary>
        /// 最新的注册用户
        /// </summary>
        /// <param name="registrationDate">注册用户</param>
        /// <returns></returns>
        [HttpGet("Newest")]
        public JsonResult GetMyFriends(DateTime? registrationDate)
        {
            registrationDate ??= DateTime.Now;
            return OkData(_friendService.GetNewestUsers((DateTime)registrationDate));
        }
        /// <summary>
        /// 搜索用户添加
        /// </summary>
        /// <param name="keyword">手机号、故新号</param>
        /// <returns></returns>
        [HttpGet("Search/{keyword}")]
        public JsonResult GetMyFriends(string keyword)
        {
            return OkNormalData(_friendService.SearchAddUser(keyword));
        }
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <param name="nickName">好友备注</param>
        /// <param name="resean"></param>
        /// <returns></returns>
        [HttpPost("add/{friendUserId}")]
        public WebResponseContent AddFriend([FromRoute] int friendUserId, [FromQuery] string nickName, [FromQuery] string reasean)
        {
            WebResponseContent content = _friendService.AddFriend(friendUserId, nickName, reasean);
            return content;
        }
        /// <summary>
        /// 接受添加
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns></returns>
        [HttpPut("Accept/{friendUserId}")]
        public WebResponseContent AcceptFriend(int friendUserId)
        {
            WebResponseContent content = _friendService.AcceptFriend(friendUserId);
            return content;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns></returns>
        [HttpDelete("{friendUserId}")]
        public WebResponseContent DelFrinend(int friendUserId)
        {
            WebResponseContent content = WebResponseContent.Instance.Info(_friendService.DelFrinend(friendUserId), "删除成功");
            return content;
        }
        /// <summary>
        /// 好友信息[包含朋友圈，tag标签]
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns></returns>
        [HttpGet("{friendUserId}")]
        public JsonResult GetFriendInfo(int friendUserId)
        {
            return OkData(_friendService.GetFriendInfo(friendUserId));
        }
        [HttpGet("detail/{friendUserId}")]
        public JsonResult GetFriendDetail(int friendUserId)
        {
            return JsonNormal(WebResponseNormal.Instance.OK(_friendService.GetFriendDetail(friendUserId)));
        }

        /// <summary>
        /// 获取好友-树形
        /// </summary>
        /// <returns></returns>
        [HttpGet("Tree")]
        public JsonResult GetMyFriends()
        {
            return OkData(_friendService.GetMyFriends());
        }
        ///// <summary>
        ///// 搜索本地好友
        ///// </summary>
        ///// <param name="friendStatus"></param>
        ///// <param name="keyword"></param>
        ///// <param name="addDate"></param>
        ///// <returns></returns>
        //[HttpGet("Search")]
        //public JsonResult GetMyFriends(FriendStatus friendStatus, string keyword, DateTime? addDate)
        //{
        //    addDate = addDate == null ? DateTime.Now : addDate;
        //    return OkData(_friendService.GetMyFriends(friendStatus, keyword, (DateTime)addDate));
        //}
        [HttpGet("All/ToMe")]
        public JsonResult GetAllMyFriendsByAddMe()
        {
            return OkData(_friendService.GetAllFriendsToMe());
        }
        [HttpGet("All/ToOther")]
        public JsonResult GetAllMyFriends()
        {
            return JsonNormal(WebResponseNormal.Instance.OK(_friendService.GetAllFriendsToOther()));
        }
        /// <summary>
        /// 搜索待我同意好友的用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("TobeAgree")]
        public JsonResult GetTobeAgreeFriends()
        {
            return OkData(_friendService.GetToBeAgreeFriends());
        }
        [HttpGet("BlackList")]
        public JsonResult GetBlackListFriends()
        {
            return OkData(_friendService.GetBlackListFriends());
        }

        /// <summary>
        /// 好友设置-是否免打扰
        /// </summary>
        /// <param name="friendUserId">好友ID</param>
        /// <param name="notDisturb">是否免打扰</param> 
        /// <returns></returns>
        [HttpPut("Setting/notDisturb/{friendUserId}")]
        public JsonResult FriendSettingNotDisturb([FromRoute] int friendUserId, [FromQuery] bool notDisturb)
        {
            return OkData(_friendService.FriendSetting(friendUserId, notDisturb, null, null, null, null));
        }
        /// <summary>
        /// 好友设置-是否置顶
        /// </summary>
        /// <param name="friendUserId">好友ID</param>
        /// <param name="top">是否置顶</param> 
        /// <returns></returns>
        [HttpPut("Setting/top/{friendUserId}")]
        public JsonResult FriendSettingTop([FromRoute] int friendUserId, [FromQuery] bool top)
        {
            return OkData(_friendService.FriendSetting(friendUserId, null, top, null, null, null));
        }
        /// <summary>
        /// 好友设置-是否拉黑
        /// </summary>
        /// <param name="friendUserId">好友ID</param> 
        /// <param name="black">是否拉黑</param> 
        /// <returns></returns>
        [HttpPut("Setting/black/{friendUserId}")]
        public JsonResult FriendSettingBlack([FromRoute] int friendUserId, [FromQuery] bool black)
        {
            return OkData(_friendService.FriendSetting(friendUserId, null, null, black, null, null));
        }
        /// <summary>
        /// 好友设置-图片地址
        /// </summary>
        /// <param name="friendUserId">好友ID</param> 
        /// <param name="backGroundImg">图片地址</param>
        /// <returns></returns>
        [HttpPut("Setting/backGroundImg/{friendUserId}")]
        public JsonResult FriendSettingImage([FromRoute] int friendUserId, [FromQuery] string backGroundImg)
        {
            return OkData(_friendService.FriendSetting(friendUserId, null, null, null, null, backGroundImg));
        }
        /// <summary>
        /// 好友设置-备注昵称
        /// </summary>
        /// <param name="friendUserId">好友ID</param>
        /// <param name="remarkName">备注昵称</param>
        /// <returns></returns>
        [HttpPut("Setting/remarkName/{friendUserId}")]
        public JsonResult FriendSettingRemark([FromRoute] int friendUserId, [FromQuery] string remarkName)
        {
            return OkData(_friendService.FriendSetting(friendUserId, null, null, null, remarkName, null));
        }
    }
}
