/*
 *Author：jxx
 *Contact：283591387@qq.com
 *Date：2018-07-01
 * 此代码由框架生成，请勿随意更改
 */
using GuXin.System.IRepositories;
using GuXin.Core.BaseProvider;
using GuXin.Core.EFDbContext;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.System.Repositories
{
    public partial class Sys_DictionaryListRepository : RepositoryBase<Sys_DictionaryList>, ISys_DictionaryListRepository
    {
        public Sys_DictionaryListRepository(GuXinContext dbContext)
        : base(dbContext)
        {

        }
        public static ISys_DictionaryListRepository Instance
        {
            get { return AutofacContainerModule.GetService<ISys_DictionaryListRepository>(); }
        }
    }
}

