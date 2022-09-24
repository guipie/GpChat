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
using GuXin.Business.IServices;
using GuXin.Core.Extensions;
using GuXin.Core.Utilities;
using GuXin.Entity.AppEnum;

namespace GuXin.App.Controllers
{
    [Route("System")]
    public class SystemController : AppApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IComplaintService service;

        private readonly IApp_UpgradeService upgradeService;
        private readonly ISys_AgreementService agreementService;
        [ActivatorUtilitiesConstructor]
        public SystemController(
            IHttpContextAccessor httpContextAccessor, IComplaintService complaintService, IApp_UpgradeService _UpgradeService, ISys_AgreementService _AgreementService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            service = complaintService;
            upgradeService = _UpgradeService;
            agreementService = _AgreementService;
        }
        [HttpPost("Complaint")]
        public JsonResult Post([FromBody] Complaint complaint)
        {
            var result = service.Post(complaint);
            result.SuccessMessage = "投诉成功";
            return Json(result);
        }
        /// <summary>
        /// 版本升级
        /// </summary>
        /// <param name="platform">android或ios或pc</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        [HttpGet("Upgrade/{platform}/{version}"), AllowAnonymous]
        public WebResponseContent Upgrade([FromRoute] string platform, [FromRoute] string version)
        {
            return upgradeService.App_Upgrade(platform, version);
        }

        /// <summary>
        /// 协议
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("Agreement/{key}")]
        public WebResponseContent Agreement([FromRoute] Agreement key)
        {
            return WebResponseContent.Instance.OK(agreementService.AgreementInfo(key));
        }
    }
}
