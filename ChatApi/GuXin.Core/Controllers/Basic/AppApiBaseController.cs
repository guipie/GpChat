using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuXin.Core.Filters;
using GuXin.Core.Utilities;
using GuXin.ImCore;
using GuXin.Core.ManageUser;

namespace GuXin.Core.Controllers.Basic
{
    [AppJWTAuthorize, FixedToken, ApiController]
    public class AppApiBaseController : Controller
    {
        protected void SendNotice(int receiveUserId, ChatType type, string content = "")
        {
            int userId = UserContext.Current.UserId;
            ChatObject sendObject = new()
            {
                SendTime = DateTime.Now,
                SendUserId = userId,
                ReceiveUserId = receiveUserId,
                Type = type,
                Content = content ?? "1"
            };
            Task.Factory.StartNew(() =>
            {
                ImHelper.SendMessage(userId.ToClientGuid(), new[] { receiveUserId.ToClientGuid() }, sendObject);
            });
        }
        /// <summary>
        /// json原格式返回数据(默认是驼峰格式)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        protected JsonResult JsonNormal(object data, JsonSerializerSettings serializerSettings = null)
        {
            serializerSettings ??= new JsonSerializerSettings();
            serializerSettings.ContractResolver = null;
            return Json(data, serializerSettings);
        }

        protected JsonResult OkData(IQueryable<object> data)
        {
            return Json(WebResponseContent.Instance.OK("数据列表", data));
        }
        protected JsonResult OkData(object data, string message = "")
        {
            message ??= "获取数据成功";
            return Json(WebResponseContent.Instance.OK(message, data));
        }
        protected JsonResult OkNormalData(IQueryable<object> data)
        {
            return JsonNormal(WebResponseNormal.Instance.OK("数据列表", data));
        }
        protected JsonResult OkNormalData(object data, string message = "")
        {
            message ??= "获取数据成功";
            return JsonNormal(WebResponseNormal.Instance.OK(message, data));
        }
        protected JsonResult OkDataShow(bool status, string succeedMessage = "")
        {
            var content = WebResponseContent.Instance;
            content.Status = status;
            content.SuccessMessage = string.IsNullOrWhiteSpace(succeedMessage) ? "操作成功" : succeedMessage;
            return Json(content);
        }

        protected JsonResult OkData(int id, string message = "")
        {
            if (id == 0) message ??= "操作失败";
            return Json(WebResponseContent.Instance.OK(message, id));
        }
    }
}
