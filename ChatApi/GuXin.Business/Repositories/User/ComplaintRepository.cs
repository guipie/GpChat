/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹ComplaintRepository编写代码
 */
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using GuXin.Business.IRepositories;

namespace GuXin.Business.Repositories
{
    public partial class ComplaintRepository : RepositoryBase<Complaint>, IComplaintRepository
    {
        public ComplaintRepository(GuXinContext dbContext)
        : base(dbContext)
        {

        }
        public static IComplaintRepository Instance
        {
            get { return AutofacContainerModule.GetService<IComplaintRepository>(); }
        }
    }
}
