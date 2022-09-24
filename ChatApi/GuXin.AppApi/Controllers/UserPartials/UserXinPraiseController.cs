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
using GuXin.Entity.AppEnum;

namespace GuXin.App.Controllers
{
    [Route("User/XinZan")]
    public partial class UserController : AppApiBaseController
    {

        /// <summary>
        /// 个人新赞统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetInfo()
        {
            return OkData(_user_XinPraiseService.GetInfo());
        }
        /// <summary>
        /// 个人新赞更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> UpdateAsync(XinPraiseType xinPraiseType = XinPraiseType.Login)
        {
            return OkData(await _user_XinPraiseService.PostAsync(xinPraiseType));
        }
    }
}
