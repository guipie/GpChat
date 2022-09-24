/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("Content_File",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    public partial class Content_FileController
    {
        private readonly IContent_FileService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public Content_FileController(
            IContent_FileService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
