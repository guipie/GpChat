/*
 *所有关于Content_Collect类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Content_CollectService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using GuXin.Business.IRepositories;
using System;
using GuXin.Core.ManageUser;
using GuXin.Entity.AppDto.Output;
using GuXin.Business.IServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GuXin.Business.Services
{
    public partial class Content_CollectService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_CollectRepository _repository;
        private readonly IContentRepository _contentRepository;

        [ActivatorUtilitiesConstructor]
        public Content_CollectService(
            IContent_CollectRepository dbRepository,
             IContentRepository contentRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _contentRepository = contentRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public WebResponseContent PostCollect(Guid contentGuid)
        {
            WebResponseContent webResponse = WebResponseContent.Instance.OK();
            Content entity = _contentRepository.FindAsIQueryable(m => m.ContentGuid == contentGuid).AsTracking().FirstOrDefault();
            if (_repository.Exists(m => m.CreateID == UserContext.Current.UserId && m.ContentGuid == contentGuid))
            {
                _repository.Delete(m => m.CreateID == UserContext.Current.UserId && m.ContentGuid == contentGuid);
                entity.CollectCount--;
                webResponse.Status = _repository.SaveChanges() > 0;
                webResponse.SuccessMessage = "已取消收藏";
                webResponse.Data = -1;
                return webResponse;
            }
            else
            {
                entity.CollectCount++;
                _repository.Add(new Content_Collect() { ContentGuid = contentGuid }.SetCreateDefaultVal());
                webResponse.Status = _repository.SaveChanges() > 0;
                webResponse.SuccessMessage = "已收藏";
                webResponse.Data = 1;
                return webResponse;
            }
        }

    }
}
