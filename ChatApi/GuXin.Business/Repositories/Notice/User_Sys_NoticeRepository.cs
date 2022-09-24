/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹User_Sys_NoticeRepository编写代码
 */
using GuXin.Business.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Repositories
{
    public partial class User_Sys_NoticeRepository : RepositoryBase<User_Sys_Notice> , IUser_Sys_NoticeRepository
    {
    public User_Sys_NoticeRepository(GuXinContext dbContext)
    : base(dbContext)
    {

    }
    public static IUser_Sys_NoticeRepository Instance
    {
      get {  return AutofacContainerModule.GetService<IUser_Sys_NoticeRepository>(); } }
    }
}
