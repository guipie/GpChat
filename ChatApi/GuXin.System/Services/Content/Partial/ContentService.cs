/*
 *所有关于Content类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*ContentService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using GuXin.Entity.DomainModels;
using System.Linq;
using System.Linq.Expressions;
using GuXin.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.AppDto.Output;
using GuXin.Core.ManageUser;
using GuXin.Entity.AppDto.Common;
using GuXin.System.IRepositories;
using System.Threading.Tasks;
using GuXin.Entity.AppEnum;

namespace GuXin.System.Services
{
    public partial class ContentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContentRepository _repository;//访问数据库 

        private readonly IContent_FileRepository _FileRepository;
        [ActivatorUtilitiesConstructor]
        public ContentService(
            IContentRepository dbRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
    }
}
