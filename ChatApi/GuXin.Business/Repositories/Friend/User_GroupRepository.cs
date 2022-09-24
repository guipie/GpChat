
using GuXin.Business.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Repositories
{
    public partial class User_GroupRepository : RepositoryBase<User_Group>, IUser_GroupRepository
    {
        public User_GroupRepository(GuXinContext dbContext)
        : base(dbContext)
        {

        }
        public static IUser_GroupRepository Instance
        {
            get { return AutofacContainerModule.GetService<IUser_GroupRepository>(); }
        }
    }
}
