/*
 *所有关于Content_Intro类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Content_IntroService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using System.Linq;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using GuXin.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using GuXin.Entity.AppDto.Input;
using System.Collections.Generic;
using GuXin.Core.ManageUser;
using GuXin.System.IRepositories;

namespace GuXin.System.Services
{
    public partial class Content_IntroService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_IntroRepository _repository;//访问数据库
        private readonly IContent_Intro_PraiseRepository _Intro_PraiseRepository;//访问数据库
        private readonly IContent_FileRepository _FileRepository;

        [ActivatorUtilitiesConstructor]
        public Content_IntroService(
            IContent_IntroRepository dbRepository, IContent_FileRepository content_FileRepository, IContent_Intro_PraiseRepository content_Intro_PraiseRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _FileRepository = content_FileRepository;
            _Intro_PraiseRepository = content_Intro_PraiseRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

    }
}
