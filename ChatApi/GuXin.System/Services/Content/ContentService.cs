/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下ContentService与IContentService中编写
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GuXin.Core.BaseProvider;
using GuXin.Core.Enums;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.AppDto.Output;
using GuXin.Entity.DomainModels;
using GuXin.System.IRepositories;
using GuXin.System.IServices;

namespace GuXin.System.Services
{
    public partial class ContentService : ServiceBase<Content, IContentRepository>
    , IContentService, IDependency
    {
        public ContentService(IContentRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IContentService Instance
        {
            get { return AutofacContainerModule.GetService<IContentService>(); }
        }
    }
}
