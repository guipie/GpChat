/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Sys_ProvinceService与ISys_ProvinceService中编写
 */
using GuXin.System.IRepositories;
using GuXin.System.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using System.Linq;

namespace GuXin.System.Services
{
    public partial class Sys_ProvinceService : ServiceBase<Sys_Province, ISys_ProvinceRepository>
    , ISys_ProvinceService, IDependency
    {
        public Sys_ProvinceService(ISys_ProvinceRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static ISys_ProvinceService Instance
        {
            get { return AutofacContainerModule.GetService<ISys_ProvinceService>(); }
        }

    }
}
