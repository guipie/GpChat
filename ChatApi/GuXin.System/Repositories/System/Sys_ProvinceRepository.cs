/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *Repository提供数据库操作，如果要增加数据库操作请在当前目录下Partial文件夹Sys_ProvinceRepository编写代码
 */
using GuXin.System.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Repositories
{
    public partial class Sys_ProvinceRepository : RepositoryBase<Sys_Province> , ISys_ProvinceRepository
    {
    public Sys_ProvinceRepository(GuXinContext dbContext)
    : base(dbContext)
    {

    }
    public static ISys_ProvinceRepository Instance
    {
      get {  return AutofacContainerModule.GetService<ISys_ProvinceRepository>(); } }
    }
}
