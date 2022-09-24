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
using GuXin.Business.IServices;
using GuXin.Core.Utilities;
using GuXin.Core.ManageUser;
using GuXin.ImCore;
using GuXin.Core.Extensions;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 用户群
    /// </summary>
    [Route("Group")]
    public class GroupController : AppApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_GroupService _GroupService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        [ActivatorUtilitiesConstructor]
        public GroupController(
            IHttpContextAccessor httpContextAccessor,
            IUser_GroupService user_GroupService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _GroupService = user_GroupService;
        }
        [HttpGet]
        public JsonResult GetALL()
        {
            return OkNormalData(_GroupService.GetAllGroupsInfo());
        }
        /// <summary>
        /// 获取保存到群聊的群
        /// </summary>
        /// <returns></returns>
        [HttpGet("Save")]
        public JsonResult GetSaveMyGroups()
        {
            return OkData(_GroupService.GetMySaveGroups());
        }

        [HttpGet("{groupId}")]
        public JsonResult Get([FromRoute] int groupId)
        {
            var result = WebResponseNormal.Instance.OK(_GroupService.GetGroupInfo(groupId));
            return JsonNormal(result);
        }
        /// <summary>
        /// 创建群聊
        /// </summary>
        /// <param name="userIds">群用户</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post([FromBody] int[] userIds)
        {
            var result = _GroupService.Post(userIds);
            if (result.Status)
            {
                foreach (int userId in userIds)
                    ImHelper.JoinChan(userId.ToClientGuid(), result.Data.ToString());
                ImHelper.JoinChan(UserContext.Current.UserId.ToClientGuid(), result.Data.ToString());
            }
            return Json(result);
        }
        [HttpPut("{groupId}")]
        public JsonResult Put([FromRoute] int groupId, [FromBody] int[] userIds)
        {
            return JsonNormal(_GroupService.Put(groupId, userIds));
        }
        [HttpDelete("{groupId}")]
        public JsonResult Delete([FromRoute] int groupId, [FromBody] int[] userIds)
        {
            return Json(_GroupService.Delete(groupId, userIds));
        }

        [HttpDelete("Quit/{groupId}")]
        public JsonResult Quit([FromRoute] int groupId)
        {
            return Json(_GroupService.Quit(groupId));
        }
        [HttpPost("{groupId}")]
        public JsonResult GroupNameDiscription([FromRoute] int groupId, string name, string discription)
        {
            return OkData(_GroupService.GroupNameDiscription(groupId, name, discription));
        }
        [HttpPost("Settings/{groupId}")]
        public JsonResult GroupSettings([FromRoute] int groupId, bool? notDisturb, bool? top, bool? saveMy, bool? accept)
        {
            return OkData(_GroupService.GroupSettings(groupId, notDisturb, top, saveMy, accept));
        }
        [HttpPost("Settings/NickName/{groupId}")]
        public JsonResult GroupSetNickName([FromRoute] int groupId, string nickName)
        {
            return OkData(_GroupService.SetNickName(groupId, nickName));
        }
    }
}
