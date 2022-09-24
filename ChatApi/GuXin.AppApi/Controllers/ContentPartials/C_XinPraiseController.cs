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
using GuXin.Core.Utilities;

namespace GuXin.App.Controllers
{
    /// <summary>
    ///内容 赞/新赞
    /// </summary>
    public partial class ContentsController : AppApiBaseController
    {

        /// <summary>
        /// 新赞公共内容
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <returns></returns>
        [HttpPost("XinPraise/{contentGuid}")]
        public WebResponseContent AddXinPraise([FromRoute] Guid contentGuid)
        {
            return _content_XinPraiseService.PostContentXinZan(contentGuid);
        }
    }
}
