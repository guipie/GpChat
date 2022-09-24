/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Content_XinPraiseService与IContent_XinPraiseService中编写
 */
using System;
using System.Linq;
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Services
{
    public partial class Content_XinPraiseService : ServiceBase<Content_XinPraise, IContent_XinPraiseRepository>
    , IContent_XinPraiseService, IDependency
    {
        public Content_XinPraiseService(IContent_XinPraiseRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IContent_XinPraiseService Instance
        {
            get { return AutofacContainerModule.GetService<IContent_XinPraiseService>(); }
        }

       
    }
}
