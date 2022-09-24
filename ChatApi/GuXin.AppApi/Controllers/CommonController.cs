using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using GuXin.Core.Controllers.Basic;
using GuXin.System.IServices;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 公共接口文档
    /// </summary>
    [Route("Common")]
    public class CommonController : AppApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISys_ProvinceService _ProvinceService;
        //private readonly IUser_Tag_MemberService _tagMemberService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="provinceService"></param>
        [ActivatorUtilitiesConstructor]
        public CommonController(
            IHttpContextAccessor httpContextAccessor,
            ISys_ProvinceService provinceService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _ProvinceService = provinceService;
        }
        /// <summary>
        /// 省
        /// </summary>
        /// <returns></returns>
        [HttpGet("Provinces")]
        public IActionResult Provinces()
        {
            return OkData(_ProvinceService.GetProvinces());
        }
        /// <summary>
        /// 市区
        /// </summary>
        /// <returns></returns>
        [HttpGet("Cities/{Pcode}")]
        public IActionResult Cities([FromRoute] string Pcode)
        {
            return OkData(_ProvinceService.GetCities(Pcode));
        }
    }
}
