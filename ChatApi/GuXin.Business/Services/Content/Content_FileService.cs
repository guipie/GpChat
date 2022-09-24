/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Content_FileService与IContent_FileService中编写
 */
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using System;
using System.Linq;

namespace GuXin.Business.Services
{
    public partial class Content_FileService : ServiceBase<Content_File, IContent_FileRepository>
    , IContent_FileService, IDependency
    {
        public Content_FileService(IContent_FileRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IContent_FileService Instance
        {
            get { return AutofacContainerModule.GetService<IContent_FileService>(); }
        }

    }
}
