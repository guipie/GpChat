/*
 *所有关于Content_XinPraise类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Content_XinPraiseService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using GuXin.Core.ManageUser;
using System;
using GuXin.Core.CacheManager;
using System.Threading.Tasks;
using GuXin.ImCore;

namespace GuXin.Business.Services
{
    public partial class Content_XinPraiseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_XinPraiseRepository _repository;//访问数据库 
        private readonly ICacheService _cacheService;
        private readonly IContentRepository _contentRepository;
        [ActivatorUtilitiesConstructor]
        public Content_XinPraiseService(
            IContent_XinPraiseRepository dbRepository,
            IContentRepository contentRepository,
            ICacheService cacheService,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _contentRepository = contentRepository;
            _cacheService = cacheService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        public WebResponseContent PostContentXinZan(Guid contentGuid)
        {
            var userId = UserContext.Current.UserId;
            if (_repository.Exists(m => m.ContentGuid == contentGuid && m.CreateID == userId))
                return WebResponseContent.Instance.Error("不能重复新赞.");
            //今日使用新赞数
            int usedCount = _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId && m.CreateDate.DayOfYear == DateTime.Now.DayOfYear).Count();
            var currentXinZanCount = UserContext.Current.UserInfo.XinZan;
            int nextUsedCount = currentXinZanCount[currentXinZanCount.Count - 1 - usedCount < 0 ? 0 : currentXinZanCount.Count - 1 - usedCount];
            _repository.Add(new Content_XinPraise() { ContentGuid = contentGuid, Quantity = nextUsedCount }.SetCreateDefaultVal(), true);

            var content = _contentRepository.FindAsIQueryable(m => m.ContentGuid == contentGuid).AsTracking().FirstOrDefault();
            content.XinPraiseCount = content.XinPraiseCount + nextUsedCount;
            _contentRepository.SaveChanges();

            //推送通知
            var setting = _repository.DbContext.Set<User_Interact_Setting>().FirstOrDefault(m => m.CreateID == content.CreateID);
            if (setting == null || !setting.Disable)
                Task.Factory.StartNew(() => { ImHelper.SendNotice(UserContext.Current.UserId, content.CreateID, UserContext.Current.NickName + "新赞了您", ChatType.XinPraise); });
            return WebResponseContent.Instance.OK("新赞成功", nextUsedCount);
        }

        public IQueryable<object> GetXinPraises(Guid contentGuid, DateTime searchDate)
        {
            return _repository.FindAsIQueryable(m => m.ContentGuid == contentGuid && m.CreateDate <= searchDate).OrderByDescending(m => m.CreateDate).Take(10);
        }

    }
}
