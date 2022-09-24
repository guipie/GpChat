using GuXin.Business.IServices;
using GuXin.Core.Controllers.Basic;
using GuXin.Core.Extensions;
using GuXin.Core.ManageUser;
using GuXin.Core.Utilities;
using GuXin.ImCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GuXin.AppApi.Controllers
{
    /// <summary>
    /// 消息推送
    /// </summary>
    [Route("WS")]
    public class WebSocketController : AppApiBaseController
    {
        private readonly IUserService _service;//访问业务代码
        private readonly IUser_FriendService _FriendService;
        private readonly IUser_GroupService _GroupService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="httpContextAccessor"></param>
        [ActivatorUtilitiesConstructor]
        public WebSocketController(
            IUserService service,
            IUser_FriendService friendService,
            IUser_GroupService groupService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _service = service;
            _FriendService = friendService;
            _GroupService = groupService;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 请求IP
        /// </summary>
        public string Ip => Request.Headers["X-Real-IP"].FirstOrDefault() ?? Request.HttpContext.Connection.RemoteIpAddress.ToString();

        /// <summary>
        /// 初始化获取websocket分区
        /// </summary> 
        /// <returns></returns>
        [HttpPost("Init")]
        public WebResponseContent SocketInit()
        {
            var currentGuid = UserContext.Current.UserId.ToClientGuid();
            var wsserver = ImHelper.PrevConnectServer(currentGuid, Ip);
            if (wsserver.IsNullOrEmpty()) WebResponseContent.Instance.Error("chat初始化失败");
            _GroupService.GetAllGroups().ToList().ForEach(m =>
            {
                ImHelper.JoinChan(currentGuid, m.ToString());
            });
            var result = WebResponseContent.Instance.OK("chat初始化成功", new
            {
                currentSocketGuid = currentGuid,
                wsserver,
                chat = ImHelper.GetUnreadMessage(currentGuid)
            });
            return result;
        }
        [HttpGet("Chat/Reload")]
        public WebResponseContent ChatReload()
        {
            var currentGuid = UserContext.Current.UserId.ToClientGuid();
            return WebResponseContent.Instance.OK("拉取聊天记录", ImHelper.GetUnreadMessage(currentGuid));
        }
        /// <summary>
        /// im发消息
        /// </summary>
        /// <param name="sendObject">发送对象</param>
        /// <param name="IsReceive">是否回执告诉客户端</param>
        /// <returns></returns>
        [HttpPost("send-msg")]
        public WebResponseContent Sendmsg([FromBody] ChatObject sendObject, [FromQuery] bool IsReceive = false)
        {
            sendObject.SendTime ??= DateTime.Now;
            sendObject.IsGroup = 0;
            var SendUserId = UserContext.Current.UserId;
            if (sendObject.SendUserId != SendUserId) return WebResponseContent.Instance.Error("非法请求");
            if (sendObject.SendUserId == sendObject.ReceiveUserId) return WebResponseContent.Instance.Error("自己发给自己");

            WebResponseContent responseContent = _FriendService.SendVerification(sendObject.ReceiveUserId);
            if (responseContent.Status)
                ImHelper.SendMessage(SendUserId.ToClientGuid(), new[] { sendObject.ReceiveUserId.ToClientGuid() }, sendObject, IsReceive);
            return responseContent;
        }
        [HttpGet("get-msg")]
        public WebResponseNormal GetMsg()
        {
            return WebResponseNormal.Instance.OK("拉取消息", ImHelper.GetMessage(UserContext.Current.UserId.ToClientGuid()));
        }
        /// <summary>
        /// 推送通知
        /// </summary> 
        /// <returns></returns>
        [HttpPost("send-notice/{userId}/{receiveUserId}")]
        public WebResponseContent SendNotice([FromRoute] int userId, [FromRoute] int receiveUserId, [FromQuery] ChatType type)
        {
            var SendUserId = UserContext.Current.UserId;
            if (userId != SendUserId) return WebResponseContent.Instance.Error("非法请求");
            if (receiveUserId == SendUserId) return WebResponseContent.Instance.Error("自己发给自己");

            ChatObject sendObject = new ChatObject() { SendTime = DateTime.Now, SendUserId = userId, ReceiveUserId = receiveUserId, Type = type };
            ImHelper.SendMessage(SendUserId.ToClientGuid(), new[] { receiveUserId.ToClientGuid() }, sendObject);
            return WebResponseContent.Instance.OK();
        }
        ///// <summary>
        ///// 群聊，获取群列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("get-channels")]
        //public object getChannels()
        //{
        //    return new
        //    {
        //        code = 0,
        //        channels = ImHelper.GetChanList().Select(a => new { a.chan, a.online })
        //    };
        //}

        ///// <summary>
        ///// 群聊，绑定消息频道
        ///// </summary>
        ///// <param name="websocketId">本地标识，若无则不传，接口会返回，请保存本地重复使用</param>
        ///// <param name="channel">消息频道</param>
        ///// <returns></returns>
        [HttpPost("subscr-channel/{websocketId}/{groupId}")]
        public WebResponseContent subscrChannel([FromRoute] Guid websocketId, [FromRoute] string groupId)
        {
            ImHelper.JoinChan(websocketId, groupId);
            return WebResponseContent.Instance.OK("绑定成功");
        }

        ///// <summary>
        ///// 群聊，发送频道消息，绑定频道的所有人将收到消息
        ///// </summary>
        ///// <param name="channel">消息频道</param>
        ///// <param name="content">发送内容</param>
        ///// <returns></returns>
        [HttpPost("send-channelmsg/{websocketId}/{groupId}")]
        public object sendChannelmsg([FromRoute] Guid websocketId, [FromRoute] string groupId, [FromBody] ChatObject sendObject, [FromQuery] bool receive = false)
        {
            sendObject.SendTime ??= DateTime.Now;
            sendObject.IsGroup = 1;
            if (ImHelper.GetChanListByClientId(websocketId).Contains(groupId))
            {
                ImHelper.SendChanMessage(websocketId, groupId, sendObject, receive);
                return WebResponseContent.Instance.OK("群消息发送成功");
            }
            return WebResponseContent.Instance.Error("您被移除了群聊");
        }
    }
}
