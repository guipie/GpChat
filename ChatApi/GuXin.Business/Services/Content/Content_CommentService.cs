/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Content_CommentService与IContent_CommentService中编写
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
    public partial class Content_CommentService : ServiceBase<Content_Comment, IContent_CommentRepository>
    , IContent_CommentService, IDependency
    {
        public Content_CommentService(IContent_CommentRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IContent_CommentService Instance
        {
            get { return AutofacContainerModule.GetService<IContent_CommentService>(); }
        }

    
    }
}
