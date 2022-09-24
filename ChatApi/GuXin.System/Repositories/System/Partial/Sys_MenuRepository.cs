using GuXin.System.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Core.EFDbContext;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Repositories
{
    public partial class Sys_MenuRepository
    {
        public override GuXinContext DbContext => base.DbContext;
    }
}

