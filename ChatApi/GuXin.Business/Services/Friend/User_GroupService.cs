/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下User_Group_SettingService与IUser_Group_SettingService中编写
 */
using System.Linq;
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Core.Utilities;
using GuXin.Entity.AppDto.Output;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Services
{
    public partial class User_GroupService : ServiceBase<User_Group_Setting, IUser_Group_SettingRepository>
    , IUser_GroupService, IDependency
    {
        public User_GroupService(IUser_Group_SettingRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IUser_GroupService Instance
        {
            get { return AutofacContainerModule.GetService<IUser_GroupService>(); }
        }

    }
}
