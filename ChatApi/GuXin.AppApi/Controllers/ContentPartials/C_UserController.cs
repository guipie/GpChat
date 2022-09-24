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
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.DomainModels;
using GuXin.Core.ManageUser;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 用户内容相关
    /// </summary> 
    public partial class ContentsController : AppApiBaseController
    {
        /// <summary>
        /// 获取用户内容
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet("user/{userId:int?}")]
        public async Task<JsonResult> GetAsync([FromRoute] int? userId, [FromQuery] DateTime? dateTime)
        {
            userId = userId == null ? UserContext.Current.UserId : userId;
            return OkData(await _contentService.GetAsync((int)userId, dateTime ?? DateTime.Now));
        }
        /// <summary>
        /// 用户优质内容
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet("user/top/{userId:int?}")]
        public async Task<JsonResult> GetTopAsync([FromRoute] int? userId, [FromQuery] DateTime? dateTime)
        {
            userId = userId == null ? UserContext.Current.UserId : userId;
            return OkData(await _contentService.GetTopAsync((int)userId, dateTime ?? DateTime.Now));
        }
        /// <summary>
        /// 新增内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post([FromBody] ContentInput dto)
        {
            return Json(_contentService.Post(dto));
        }
        /// <summary>
        /// 修改内容
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult Put([FromBody] ContentInput dto)
        {
            return OkData(_contentService.Put(dto));
        }
        /// <summary>
        /// 删除内容
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete("{guid}")]
        public JsonResult Del([FromRoute] Guid guid)
        {
            return Json(_contentService.Del(guid));
        }
        [HttpGet("edit/{guid}")]
        public JsonResult Get([FromRoute] Guid guid)
        {
            return OkNormalData(_contentService.EditDetail(guid));
        }
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("Statistic")]
        public JsonResult Statistic()
        {
            return OkData(_contentService.Statistic());
        }
        /// <summary>
        /// 我的互动内容
        /// </summary>
        /// <returns></returns>
        [HttpGet("Interact")]
        public async Task<JsonResult> InteractAsync(DateTime? searchDate, int type)
        {
            return OkData(await _contentService.InteractContentsAsync(searchDate ?? DateTime.Now, type));
        }
    }
}
