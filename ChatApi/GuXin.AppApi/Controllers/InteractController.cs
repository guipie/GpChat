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
using GuXin.Entity.DomainModels;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 互动相关接口
    /// </summary>
    [Route("Interact")]
    public class InteractController : AppApiBaseController
    {
        private readonly IUser_Interact_SettingService _settingService;//互动设置访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IContentService _contentService;
        [ActivatorUtilitiesConstructor]
        public InteractController(
            IUser_Interact_SettingService settingService,
            IContentService contentService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _settingService = settingService;
            _contentService = contentService;
            _httpContextAccessor = httpContextAccessor;
        }
        #region 互动设置
        [HttpGet]
        [Route("Settings")]
        public JsonResult GetUserSettings()
        {
            return OkNormalData(_settingService.GetCurrentSettings());
        }
        [HttpPut]
        [Route("Settings/Top/{top}")]
        public WebResponseContent SetTop([FromRoute] bool top)
        {
            return WebResponseContent.Instance.Info(_settingService.IsTop(top));
        }
        [HttpPut("Settings/NotDisturb/{notDisturb}")]
        public WebResponseContent SetNotDisturb([FromRoute] bool notDisturb)
        {
            return WebResponseContent.Instance.Info(_settingService.IsNotDisturb(notDisturb));
        }
        [HttpPut("Settings/Disable/{disable}")]
        public WebResponseContent SetDisable([FromRoute] bool disable)
        {
            return WebResponseContent.Instance.Info(_settingService.IsDisable(disable));
        }

        #endregion 互动设置

        #region 互动通知

        [HttpPut("Notices/Clear")]
        public WebResponseContent SetClear()
        {
            return WebResponseContent.Instance.Info(_settingService.ClearInteractNotices());
        }
        [HttpGet("Notice")]
        public JsonResult InteractNotices([FromQuery] DateTime? searchDate)
        {
            return OkData(_settingService.GetInteractNotices(searchDate ?? DateTime.Now));
        }
        #endregion 
    }
}
