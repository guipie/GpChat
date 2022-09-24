/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下Content_Intro_PraiseService与IContent_Intro_PraiseService中编写
 */
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;

namespace GuXin.Business.Services
{
    public partial class Content_Intro_PraiseService : ServiceBase<Content_Intro_Praise, IContent_Intro_PraiseRepository>
    , IContent_Intro_PraiseService, IDependency
    {
    public Content_Intro_PraiseService(IContent_Intro_PraiseRepository repository)
    : base(repository)
    {
    Init(repository);
    }
    public static IContent_Intro_PraiseService Instance
    {
      get { return AutofacContainerModule.GetService<IContent_Intro_PraiseService>(); } }
    }
 }
