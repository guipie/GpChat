using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using GuXin.Business.IServices;
using GuXin.Core.Controllers.Basic;
using GuXin.Core.Utilities;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 内容相关接口
    /// </summary>
    [Route("Contents")]
    public partial class ContentsController : AppApiBaseController
    {
        private readonly IContentService _contentService;
        private readonly IContent_CommentService _content_CommentService;
        private readonly IContent_PraiseService _content_PraiseService;
        private readonly IContent_XinPraiseService _content_XinPraiseService;
        private readonly IContent_CollectService _content_CollectService;

        private readonly IUser_Interact_SettingService _Interact_SettingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="contentService"></param>
        /// <param name="content_CommentService"></param>
        /// <param name="praiseService"></param>
        /// <param name="xinPraiseService"></param>
        /// <param name="collectService"></param>
        /// <param name="user_Interact_SettingService"></param>
        [ActivatorUtilitiesConstructor]
        public ContentsController(
            IHttpContextAccessor httpContextAccessor,
            IContentService contentService,
            IContent_CommentService content_CommentService,
            IContent_PraiseService praiseService,
            IContent_XinPraiseService xinPraiseService,
            IContent_CollectService collectService,
            IUser_Interact_SettingService user_Interact_SettingService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _contentService = contentService;
            _content_CommentService = content_CommentService;
            _content_XinPraiseService = xinPraiseService;
            _content_PraiseService = praiseService;
            _content_CollectService = collectService;
            _Interact_SettingService = user_Interact_SettingService;
        }
        [HttpGet("{contentGuid}")]
        public async Task<WebResponseContent> GetContentsDetailAsync([FromRoute] Guid contentGuid)
        {
            return await _contentService.Detail(contentGuid);
        }
        /// <summary>
        /// 初始化内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Init")]
        public async Task<JsonResult> GetInitContentsAsync([FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(await _contentService.InitContentsAsync((DateTime)searchDate));
        }
        /// <summary>
        /// 推荐内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Recommend")]
        public async Task<JsonResult> GetRecommendContentsAsync([FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(await _contentService.RecommendContentsAsync((DateTime)searchDate));
        }
        /// <summary>
        /// 榜单内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Top")]
        public async Task<JsonResult> GetTopContentsAsync([FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(await _contentService.TopContentsAsync((DateTime)searchDate));
        }
        /// <summary>
        /// 关注内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet("Follow")]
        public async Task<JsonResult> GetFollowContentsAsync([FromQuery] DateTime? searchDate)
        {
            searchDate ??= DateTime.Now;
            return OkData(await _contentService.FollowContentsAsync((DateTime)searchDate));
        }

        /// <summary>
        /// 关注的某个用户内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Follow/{userId}")]
        public async Task<JsonResult> GetFollowUserContentsAsync([FromQuery] DateTime? searchDate, [FromRoute] int userId)
        {
            searchDate ??= DateTime.Now;
            return OkData(await _contentService.FollowContentsAsync((DateTime)searchDate, userId));
        }
        /// <summary>
        /// 搜索故新内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("Search/{key}")]
        public async Task<JsonResult> SearchContentsAsync([FromQuery] DateTime? searchDate, [FromRoute] string key)
        {
            return OkData(await _contentService.SearchContentsAsync(searchDate ?? DateTime.Now, key));
        }
        /// <summary>
        /// 搜索故新用户及内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("User/Search/{key}")]
        public async Task<JsonResult> SearchUserContentsAsync([FromQuery] DateTime? searchDate, [FromQuery] int? userId, [FromRoute] string key)
        {
            return OkData(await _contentService.SearchUserContentsAsync(searchDate ?? DateTime.Now, key, userId));
        }
    }
}
