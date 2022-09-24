/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹User_Group_MemberRepository编写代码
 */
using  GuXin.System.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace  GuXin.System.Repositories
{
    public partial class User_Group_MemberRepository : RepositoryBase<User_Group_Member>, IUser_Group_MemberRepository
    {
        public User_Group_MemberRepository(GuXinContext dbContext)
        : base(dbContext)
        {

        }
        public static IUser_Group_MemberRepository Instance
        {
            get { return AutofacContainerModule.GetService<IUser_Group_MemberRepository>(); }
        }
    }
}
