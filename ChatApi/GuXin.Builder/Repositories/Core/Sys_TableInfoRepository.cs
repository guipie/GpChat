using GuXin.Builder.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.Builder.Repositories
{
    public partial class Sys_TableInfoRepository : RepositoryBase<Sys_TableInfo>, ISys_TableInfoRepository
    {
        public Sys_TableInfoRepository(GuXinContext dbContext)
        : base(dbContext)
        {

        }
        public static ISys_TableInfoRepository GetService
        {
            get { return AutofacContainerModule.GetService<ISys_TableInfoRepository>(); }
        }
    }
}

