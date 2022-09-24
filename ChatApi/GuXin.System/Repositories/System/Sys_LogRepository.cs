using GuXin.System.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Core.EFDbContext;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Repositories
{
    public partial class Sys_LogRepository : RepositoryBase<Sys_Log>, ISys_LogRepository
    {
        public Sys_LogRepository(GuXinContext dbContext)
        : base(dbContext)
        {

        }
        public static ISys_LogRepository GetService
        {
            get { return AutofacContainerModule.GetService<ISys_LogRepository>(); }
        }
    }
}

