/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下User_Sys_NoticeService与IUser_Sys_NoticeService中编写
 */
using System;
using System.Linq;
using GuXin.System.IRepositories;
using GuXin.System.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Services
{
    public partial class User_Sys_NoticeService : ServiceBase<User_Sys_Notice, IUser_Sys_NoticeRepository>
    , IUser_Sys_NoticeService, IDependency
    {
        public User_Sys_NoticeService(IUser_Sys_NoticeRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IUser_Sys_NoticeService Instance
        {
            get { return AutofacContainerModule.GetService<IUser_Sys_NoticeService>(); }
        }

    }
}
