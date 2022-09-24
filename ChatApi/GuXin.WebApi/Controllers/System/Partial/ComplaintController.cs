/*
 *接口编写处...
*如果接口需要做Action的权限验证，请在Action上使用属性
*如: [ApiActionPermission("Complaint",Enums.ActionPermissionOptions.Search)]
 */
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.Entity.DomainModels;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    public partial class ComplaintController
    {
        private readonly IComplaintService _service;//访问业务代码
        private readonly IHttpContextAccessor _httpContextAccessor;

        [ActivatorUtilitiesConstructor]
        public ComplaintController(
            IComplaintService service,
            IHttpContextAccessor httpContextAccessor
        )
        : base(service)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
