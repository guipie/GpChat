/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Content_IntroService与IContent_IntroService中编写
 */
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using GuXin.System.IRepositories;
using GuXin.System.IServices;

namespace GuXin.System.Services
{
    public partial class Content_IntroService : ServiceBase<Content_Intro, IContent_IntroRepository>
    , IContent_IntroService, IDependency
    {
        public Content_IntroService(IContent_IntroRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IContent_IntroService Instance
        {
            get { return AutofacContainerModule.GetService<IContent_IntroService>(); }
        }
    }
}
