/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果要增加方法请在当前目录下Partial文件夹User_Sys_NoticeController编写
 */
using Microsoft.AspNetCore.Mvc;
using GuXin.Core.Controllers.Basic;
using GuXin.Entity.AttributeManager;
using GuXin.System.IServices;

namespace GuXin.System.Controllers
{
    [Route("api/User_Sys_Notice")]
    [PermissionTable(Name = "User_Sys_Notice")]
    public partial class User_Sys_NoticeController : ApiBaseController<IUser_Sys_NoticeService>
    {
        public User_Sys_NoticeController(IUser_Sys_NoticeService service)
        : base(service)
        {
        }
    }
}

