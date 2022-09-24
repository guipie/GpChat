/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下User_FollowService与IUser_FollowService中编写
 */
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Core.Utilities;
using GuXin.Entity.DomainModels;
using System;
using System.Linq;

namespace GuXin.Business.Services
{
    public partial class User_FollowService : ServiceBase<User_Follow, IUser_FollowRepository>
    , IUser_FollowService, IDependency
    {
        public User_FollowService(IUser_FollowRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IUser_FollowService Instance
        {
            get { return AutofacContainerModule.GetService<IUser_FollowService>(); }
        }


    }
}
