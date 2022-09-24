/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹Content_FileController编写
 */
using Microsoft.AspNetCore.Mvc;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AttributeManager;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    [Route("api/Content_File")]
    [PermissionTable(Name = "Content_File")]
    public partial class Content_FileController : ApiBaseController<IContent_FileService>
    {
        public Content_FileController(IContent_FileService service)
        : base(service)
        {
        }
    }
}

