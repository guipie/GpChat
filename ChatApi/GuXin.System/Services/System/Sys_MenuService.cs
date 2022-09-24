using GuXin.System.IRepositories;
using GuXin.System.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Services
{
    public partial class Sys_MenuService : ServiceBase<Sys_Menu, ISys_MenuRepository>, ISys_MenuService, IDependency
    {
        public Sys_MenuService(ISys_MenuRepository repository)
             : base(repository) 
        { 
           Init(repository);
        }
        public static ISys_MenuService Instance
        {
           get { return AutofacContainerModule.GetService<ISys_MenuService>(); }
        }
    }
}

