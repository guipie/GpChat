/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹User_FriendRepository编写代码
 */
using GuXin.Business.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Repositories
{
    public partial class User_FriendRepository : RepositoryBase<User_Friend> , IUser_FriendRepository
    {
    public User_FriendRepository(GuXinContext dbContext)
    : base(dbContext)
    {

    }
    public static IUser_FriendRepository Instance
    {
      get {  return AutofacContainerModule.GetService<IUser_FriendRepository>(); } }
    }
}
