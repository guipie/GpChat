/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下User_TagService与IUser_TagService中编写
 */
using System.Collections.Generic;
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Core.Utilities;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Services
{
    public partial class User_TagService : ServiceBase<User_Tag, IUser_TagRepository>
    , IUser_TagService, IDependency
    {
        public User_TagService(IUser_TagRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IUser_TagService Instance
        {
            get { return AutofacContainerModule.GetService<IUser_TagService>(); }
        }

    }
}
