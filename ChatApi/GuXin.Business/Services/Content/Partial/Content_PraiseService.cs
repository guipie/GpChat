/*
 *所有关于Content_Praise类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Content_PraiseService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using GuXin.Business.IServices;
using System.Threading.Tasks;
using GuXin.ImCore;

namespace GuXin.Business.Services
{
    public partial class Content_PraiseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_PraiseRepository _repository;
        private readonly IContentRepository _contentRepository;
        private readonly IUser_FriendRepository _FriendRepository;

        [ActivatorUtilitiesConstructor]
        public Content_PraiseService(
            IContent_PraiseRepository dbRepository, IContentRepository contentRepository, IUser_FriendRepository user_FriendRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _contentRepository = contentRepository;
            _FriendRepository = user_FriendRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        public WebResponseContent PostPraise(Guid contentGuid)
        {
            var contentEntity = _contentRepository.FindAsIQueryable(m => m.ContentGuid == contentGuid).AsTracking().FirstOrDefault();
            WebResponseContent webResponse = WebResponseContent.Instance.OK();
            Content_Praise entity = _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId && m.ContentGuid == contentGuid).AsTracking().FirstOrDefault();
            if (entity == null)
            {
                entity = new Content_Praise() { ContentGuid = contentGuid, IsPublic = true }.SetCreateDefaultVal();
                _repository.Add(entity);
                contentEntity.PraiseCount++;
                webResponse.Data = 1;
                //推送通知
                var setting = _repository.DbContext.Set<User_Interact_Setting>().FirstOrDefault(m => m.CreateID == contentEntity.CreateID);
                if (setting == null || !setting.Disable)
                    Task.Factory.StartNew(() => { ImHelper.SendNotice(UserContext.Current.UserId, contentEntity.CreateID, UserContext.Current.NickName + "点赞了您", ChatType.XinPraise); });
                return webResponse.Info(_repository.SaveChanges());
            }
            else
            {
                if (entity.IsPublic && entity.IsFriend)
                {
                    entity.IsPublic = false;
                    contentEntity.PraiseCount--;
                    webResponse.Data = -1;
                }
                else if (entity.IsFriend)
                {
                    contentEntity.PraiseCount++;
                    entity.IsPublic = true;
                    webResponse.Data = 1;
                }
                else
                {
                    webResponse.Data = -1;
                    contentEntity.PraiseCount--;
                    _repository.DeleteWithKeys(new object[] { entity.Id });
                }
                return webResponse.Info(_repository.SaveChanges());
            }
        }
        public WebResponseContent PostFriendPraise(Guid contentGuid)
        {
            WebResponseContent webResponse = WebResponseContent.Instance.OK();
            Content_Praise entity = _repository.FindFirst(m => m.CreateID == UserContext.Current.UserId && m.ContentGuid == contentGuid);
            if (entity?.Id > 0)
            {
                _repository.Delete(entity, true);
                webResponse.SuccessMessage = "已取消点赞";
                webResponse.Data = -1;
                return webResponse;
            }
            entity = new Content_Praise() { ContentGuid = contentGuid, IsFriend = true }.SetCreateDefaultVal();
            _repository.Add(entity);
            webResponse.Data = 1;
            return WebResponseContent.Instance.Info(_repository.SaveChanges());
        }
        public IQueryable<object> GetPraiseUsers(Guid contentGuid, DateTime searchDate)
        {
            return _repository.FindAsIQueryable(m => m.ContentGuid == contentGuid && m.CreateDate <= searchDate && m.IsPublic)
                .OrderByDescending(m => m.CreateDate)
                .Select(m => new { m.Creator, m.CreateID }).Take(100);
        }

        public IQueryable<object> GetFriendPraiseUsers(Guid contentGuid, DateTime searchDate)
        {
            return (from c in _repository.FindAsIQueryable(m => m.CreateDate <= searchDate && m.ContentGuid == contentGuid && m.IsPublic)
                    join f in _FriendRepository.FindAsIQueryable(m => m.FriendUserId == UserContext.Current.UserId) on c.CreateID equals f.CreateID
                    select new
                    {
                        NickName = f.FriendRemarkName,
                        UserId = f.FriendUserId,
                        c.UserInfo.Avatar
                    }).Take(20);
        }
    }
}
