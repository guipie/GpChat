/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下User_XinPraiseService与IUser_XinPraiseService中编写
 */
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.AppEnum;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Services
{
    public partial class User_XinPraiseService : ServiceBase<User_XinPraise, IUser_XinPraiseRepository>
    , IUser_XinPraiseService, IDependency
    {
        public User_XinPraiseService(IUser_XinPraiseRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IUser_XinPraiseService Instance
        {
            get { return AutofacContainerModule.GetService<IUser_XinPraiseService>(); }
        }

      
    }
}
