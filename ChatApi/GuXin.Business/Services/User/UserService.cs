/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下UserService与IUserService中编写
 */
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Services
{
    public partial class UserService : ServiceBase<Sys_User, IUserRepository>
    , IUserService, IDependency
    {
        public UserService(IUserRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IUserService Instance
        {
            get { return AutofacContainerModule.GetService<IUserService>(); }
        }
    }
}
