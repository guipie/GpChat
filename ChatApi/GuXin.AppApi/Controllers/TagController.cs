using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using GuXin.Business.IServices;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AppDto.Input;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 用户标签相关接口
    /// </summary>
    [Route("Tag")]
    public class TagController : AppApiBaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_TagService _tagService;
        //private readonly IUser_Tag_MemberService _tagMemberService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="tagService"></param>
        [ActivatorUtilitiesConstructor]
        public TagController(
            IHttpContextAccessor httpContextAccessor,
            IUser_TagService tagService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _tagService = tagService;
        }
        /// <summary>
        /// 添加/修改标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post([FromBody] TagDto tag)
        {
            return Json(_tagService.Post(tag));
        }
        /// <summary>
        /// 获取标签数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetTags()
        {
            return OkData(_tagService.GetTags());
        }
        /// <summary>
        /// 获取标签数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("Members/{tagId:int}")]
        public JsonResult GetMembers(int tagId)
        {
            return OkNormalData(_tagService.GetMembers(tagId));
        }
    }
}
