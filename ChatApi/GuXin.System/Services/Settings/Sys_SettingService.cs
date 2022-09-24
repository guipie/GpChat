/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Sys_SettingService与ISys_SettingService中编写
 */
using GuXin.System.IRepositories;
using GuXin.System.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Services
{
    public partial class Sys_SettingService : ServiceBase<Sys_Setting, ISys_SettingRepository>
    , ISys_SettingService, IDependency
    {
        public Sys_SettingService(ISys_SettingRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static ISys_SettingService Instance
        {
            get { return AutofacContainerModule.GetService<ISys_SettingService>(); }
        }

      
    }
}
