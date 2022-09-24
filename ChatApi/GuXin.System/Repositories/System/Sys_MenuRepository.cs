using GuXin.System.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Core.EFDbContext;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Repositories
{
    public partial class Sys_MenuRepository : RepositoryBase<Sys_Menu>, ISys_MenuRepository
    {
        public Sys_MenuRepository(GuXinContext dbContext)
        : base(dbContext)
        {

        }
        public static ISys_MenuRepository Instance
        {
            get { return AutofacContainerModule.GetService<ISys_MenuRepository>(); }
        }
    }
}

