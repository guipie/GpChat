/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("Content",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    public partial class ContentController
    {
        private readonly IContentService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public ContentController(
            IContentService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
