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
using GuXin.Business.IRepositories;
using System;
using System.Collections.Generic;
using GuXin.Entity.AppDto.Input;
using GuXin.Business.IServices;
using GuXin.Entity.AppDto.Output;
using GuXin.Core.ManageUser;
using GuXin.Entity.AppDto.Common;
using System.Threading.Tasks;
using GuXin.Entity.AppEnum;
using GuXin.Core.Utilities;

namespace GuXin.Business.Services
{
    public partial class ContentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContentRepository _repository;//访问数据库
        private readonly ISys_SettingService _settingService;

        private readonly IUserRepository _UserRepository;
        private readonly IUser_FriendRepository _friendRepository;
        private readonly IUser_FollowRepository _followRepository;
        private readonly IContent_CollectRepository _collectRepository;
        private readonly IContent_XinPraiseRepository _XinPraiseRepository;
        private readonly IContent_PraiseRepository _PraiseRepository;
        private readonly IContent_CommentRepository _CommentRepository;
        private readonly IUser_Follow_SettingRepository _Follow_SettingRepository;

        private readonly IContent_FileRepository _FileRepository;
        [ActivatorUtilitiesConstructor]
        public ContentService(
            IContentRepository dbRepository,
            ISys_SettingService sys_SettingService,
            IUser_FollowRepository followRepository,
            IUser_FriendRepository friendRepository,
            IContent_CollectRepository content_CollectRepository,
            IContent_FileRepository fileRepository,
            IContent_XinPraiseRepository xinPraiseRepository,
            IContent_PraiseRepository content_PraiseRepository,
            IContent_CommentRepository content_CommentRepository,
            IUserRepository userRepository, IUser_Follow_SettingRepository follow_SettingRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _settingService = sys_SettingService;
            _followRepository = followRepository;
            _friendRepository = friendRepository;
            _collectRepository = content_CollectRepository;
            _FileRepository = fileRepository;
            _XinPraiseRepository = xinPraiseRepository;
            _PraiseRepository = content_PraiseRepository;
            _CommentRepository = content_CommentRepository;
            _UserRepository = userRepository;
            _Follow_SettingRepository = follow_SettingRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        #region 获取公共内容
        private async Task<List<AppContent>> ContentsAsync<TKey>(Expression<Func<Content, bool>> predicate, Expression<Func<Content, TKey>> keySelector = null)
        {
            predicate = predicate.And(m => m.IsPublic);
            var result = _repository.DbContext.Set<Content>().Where(predicate);
            if (keySelector != null) result = result.OrderByDescending(keySelector);
            else result = result.OrderByDescending(m => m.CreateDate);

            var currentUserId = UserContext.Current.UserId;
            var list = await result.Take(5)
                         .Select(c => new AppContent
                         {
                             guid = c.ContentGuid,
                             parentGuid = c.ParentGuid,
                             contents = c.Contents,
                             createDate = c.CreateDate,
                             collectCount = c.CollectCount,
                             xinPraiseCount = c.XinPraiseCount,
                             praiseCount = c.PraiseCount,
                             commentCount = c.CommentCount,
                             createID = c.CreateID,
                             pics = c.ContentFiles.Select(m => new { path = m.UploadPath, length = m.Length }),
                             contentUser = new ContentUser
                             {
                                 nickName = c.UserInfo.NickName,
                                 avatar = c.UserInfo.Avatar,
                                 userId = c.CreateID,
                                 remark = c.UserInfo.Remark
                             }
                         }).ToListAsync();
            list.ForEach((x) =>
           {
               x.isXinPraised = _XinPraiseRepository.Exists(m => m.CreateID == currentUserId && m.ContentGuid == x.guid);
               x.isPraised = _PraiseRepository.Exists(m => m.CreateID == currentUserId && m.ContentGuid == x.guid && m.IsPublic);
               x.isCommented = _CommentRepository.Exists(m => m.CreateID == currentUserId && m.ContentGuid == x.guid && m.IsPublic);
               x.isCollected = _collectRepository.Exists(m => m.CreateID == currentUserId && m.ContentGuid == x.guid);
               x.isShared = _repository.Exists(m => m.CreateID == currentUserId && m.ParentGuid == x.guid);
               if (x.parentGuid != null)
               {
                   x.parent = _repository.FindAsIQueryable(m => m.ContentGuid == x.parentGuid).Select(m => new
                   {
                       pics = m.ContentFiles.Select(mm => new { path = mm.UploadPath, length = mm.Length }),
                       contentUser = new ContentUser
                       {
                           nickName = m.UserInfo.NickName,
                           avatar = m.UserInfo.Avatar,
                           userId = m.CreateID,
                           remark = m.UserInfo.Remark
                       },
                       m.Contents,
                       createID = m.CreateID,
                       guid = m.ContentGuid
                   }).FirstOrDefault();
               }
           });
            return list;
        }

        /// <summary>
        /// 内容详情
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <returns></returns>
        public async Task<WebResponseContent> Detail(Guid contentGuid)
        {
            var detail = (await ContentsAsync<Guid>(m => m.ContentGuid == contentGuid)).FirstOrDefault();
            if (detail == null) return WebResponseContent.Instance.Error("内容已被删除");
            detail.isFollowing = _followRepository.Exists(x => x.FollowUserId == detail.createID && x.CreateID == UserContext.Current.UserId);
            if (detail.parentGuid != null)
                detail.parent = _repository.FindAsIQueryable(m => m.ContentGuid == detail.parentGuid).Select(m => new
                {
                    pics = m.ContentFiles.Select(mm => new { path = mm.UploadPath, length = mm.Length }),
                    m.Contents,
                    createID = m.CreateID,
                    guid = m.ContentGuid,
                    contentUser = new ContentUser
                    {
                        nickName = m.UserInfo.NickName,
                        avatar = m.UserInfo.Avatar,
                        userId = m.CreateID,
                        remark = m.UserInfo.Remark
                    }
                }).FirstOrDefault();
            return WebResponseContent.Instance.OK(detail);
        }


        /// <summary>
        /// 故新初始化内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public async Task<List<AppContent>> InitContentsAsync(DateTime searchDate)
        {
            var result = await ContentsAsync(m => m.CreateDate <= searchDate, m => m.CreateDate);
            result.ForEach(m =>
            {
                m.isFollowing = _followRepository.Exists(x => x.FollowUserId == m.createID && x.CreateID == UserContext.Current.UserId);
            });
            return result;
        }
        /// <summary>
        /// 推荐内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public async Task<List<AppContent>> RecommendContentsAsync(DateTime searchDate)
        {
            RecommendSettingDto topSettingDto = _settingService.GetReSettings();
            Expression<Func<Content, bool>> filter = m => m.CreateDate <= searchDate && m.CreateDate >= DateTime.Now.AddDays(-10)
                                                        && m.XinPraiseCount >= topSettingDto.XinPraise
                                                        && m.PraiseCount >= topSettingDto.Praise
                                                        && m.CommentCount > topSettingDto.Comment;
            var result = await ContentsAsync<float>(filter, null);
            result.ForEach(m =>
            {
                m.isFollowing = _followRepository.Exists(x => x.FollowUserId == m.createID && x.CreateID == UserContext.Current.UserId);
            });
            return result;
        }
        /// <summary>
        /// 榜单内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public async Task<List<AppContent>> TopContentsAsync(DateTime searchDate)
        {
            TopSettingDto topSettingDto = _settingService.GetTopSettings();
            Expression<Func<Content, float>> desc = m => m.CollectCount * topSettingDto.Collect
                                           + m.ShareCount * topSettingDto.Share
                                           + m.XinPraiseCount * topSettingDto.XinPraise
                                           + m.PraiseCount * topSettingDto.Praise
                                           + m.CommentCount * topSettingDto.Comment;
            return await ContentsAsync<float>(m => m.CreateDate <= searchDate && m.CreateDate >= DateTime.Now.AddDays(-10), desc);
        }

        /// <summary>
        /// 关注内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public async Task<List<AppContent>> FollowContentsAsync(DateTime searchDate, int userId)
        {
            Expression<Func<Content, bool>> predicate = m => m.CreateDate <= searchDate;
            if (userId > 0)
                predicate = predicate.And(m => m.CreateID == userId);
            else
                predicate = predicate.And(m => _followRepository.DbContext.Set<User_Follow>().Where(x => x.CreateID == UserContext.Current.UserId).Select(x => x.FollowUserId).Contains(m.CreateID));

            var result = await ContentsAsync(predicate, m => m.CreateDate);
            result.ForEach(m =>
            {
                m.isFollowing = true;
            });
            return result;
        }

        /// <summary>
        /// 互动内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> InteractContentsAsync(DateTime searchDate, int type)
        {
            int userId = UserContext.Current.UserId;
            List<DicGuid<DateTime>> interacts = new();
            if (type == 0 || type == 1)
                interacts.AddRange(repository.DbContext.Set<Content_Praise>()
                                    .Where(m => m.CreateID == userId && m.CreateDate <= searchDate)
                                    .Take(5).OrderByDescending(m => m.CreateDate).ToList()
                                    .Select(m => new DicGuid<DateTime>(m.ContentGuid, m.CreateDate)));
            if (type == 0 || type == 2)
                interacts.AddRange(repository.DbContext.Set<Content_Comment>()
                                    .Where(m => m.CreateID == userId && m.CreateDate <= searchDate)
                                    .Take(5).OrderByDescending(m => m.CreateDate).ToList()
                                    .Select(m => new DicGuid<DateTime>(m.ContentGuid, m.CreateDate)));
            if (type == 0 || type == 3)
                interacts.AddRange(repository.DbContext.Set<Content_XinPraise>()
                                    .Where(m => m.CreateID == userId && m.CreateDate <= searchDate)
                                    .Take(5).OrderByDescending(m => m.CreateDate).ToList()
                                    .Select(m => new DicGuid<DateTime>(m.ContentGuid, m.CreateDate)));

            Expression<Func<Content, bool>> predicate = m => interacts.Select(x => x.key).ToList().Contains(m.ContentGuid);
            predicate.And(m => m.CreateDate <= searchDate);
            var result = await ContentsAsync(predicate, m => m.CreateDate);

            return result;
        }
        /// <summary>
        /// 搜索内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> SearchContentsAsync(DateTime searchDate, string key)
        {
            return await (from c in _repository.FindAsIQueryable(m => m.Contents.Contains(key) && m.CreateDate <= searchDate).Take(3)
                          join u in _UserRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable) on c.CreateID equals u.User_Id
                          join f in _followRepository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId) on u.User_Id equals f.FollowUserId into temp
                          from ff in temp.DefaultIfEmpty()
                          select new
                          {
                              u.NickName,
                              u.Avatar,
                              c.CreateID,
                              c.CreateDate,
                              c.ContentGuid,
                              c.Contents,
                              Pics = c.ContentFiles.Select(f => f.UploadPath),
                              IsFollowing = ff == null ? false : true
                          }).ToListAsync();
        }
        /// <summary>
        /// 搜索用户内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> SearchUserContentsAsync(DateTime searchDate, string key, int? userId)
        {
            if (userId > 0)
            {
                return _repository.FindAsIQueryable(m => m.CreateID == userId && m.CreateDate < searchDate && m.Contents.Contains(key)).Select(m => new
                {
                    m.CreateDate,
                    m.ContentGuid,
                    m.Contents,
                    Pics = m.ContentFiles.Select(x => x.UploadPath)
                }).OrderByDescending(m => m.CreateDate).Take(3);
            }
            else
            {
                var users = await _UserRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable && (m.UserName.Contains(key) || m.NickName.Contains(key)) && m.LastLoginDate <= searchDate)
                           .OrderByDescending(m => m.LastLoginDate).Select(m => new SearchUserContent(m.User_Id, m.NickName, m.Avatar, m.LastLoginDate)).Take(3).ToListAsync();
                users.ForEach(x =>
                {
                    x.IsFollowing = _followRepository.Exists(m => m.FollowUserId == x.UserId && m.CreateID == UserContext.Current.UserId);
                    x.ContentList = _repository.FindAsIQueryable(m => m.CreateID == x.UserId && m.IsPublic).Take(10).Select(m => new
                    {
                        m.ContentGuid,
                        m.Contents,
                        Pics = m.ContentFiles.Select(f => f.UploadPath)
                    });
                });
                return users;
            }
        }
        #endregion

        #region 朋友圈内容 
        public async Task<List<FriendContent>> FriendContentsAsync(DateTime searchDate, int userId)
        {
            Expression<Func<Content, bool>> predicate = m => m.CreateDate <= searchDate & m.IsFriend;
            if (userId > 0)
                predicate = predicate.And(m => m.CreateID == userId);
            var result = await (from c in _repository.FindAsIQueryable(predicate)
                                join f in _friendRepository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId && m.Status == FriendStatus.Friend)
                                                            .Select(m => new { m.FriendUserId, m.FriendRemarkName })
                                on c.CreateID equals f.FriendUserId
                                orderby c.CreateDate descending
                                //join u in _UserRepository.FindAsIQueryable(m => m.Enable == Entity.AppEnum.UserStatus.Enable) on c.CreateID equals u.User_Id into tt
                                select new FriendContent()
                                {

                                    ContentGuid = c.ContentGuid,
                                    ParentGuid = c.ParentGuid,
                                    Contents = c.Contents,
                                    Pics = c.ContentFiles.Select(m => m.UploadPath).ToArray(),
                                    Avatar = c.UserInfo.Avatar,
                                    NickName = f.FriendRemarkName,
                                    CreateDate = c.CreateDate,
                                    UserId = c.CreateID,
                                    PraisesCount = c.Praises.Count(m => m.IsFriend),
                                    CommentCount = c.Comments.Count(m => m.IsFriend)
                                }).Take(5).ToListAsync();
            result.ForEach(m =>
            {
                if (m.CommentCount > 0)
                    m.Comments = from cc in _CommentRepository.FindAsIQueryable(cc => cc.ContentGuid == m.ContentGuid && cc.IsFriend).Take(100)
                                 .Select(cc => new { cc.CreateID, cc.CreateDate, cc.Contents, cc.Id, cc.ParentId, cc.UserInfo.Avatar })
                                 join ff in _friendRepository.FindAsIQueryable(ff => ff.CreateID == UserContext.Current.UserId)
                                 .Select(ff => new { ff.FriendRemarkName, ff.FriendUserId })
                                 on cc.CreateID equals ff.FriendUserId
                                 orderby cc.CreateDate ascending
                                 select new
                                 {
                                     cc.Id,
                                     UserId = cc.CreateID,
                                     cc.CreateDate,
                                     cc.Contents,
                                     NickName = ff.FriendRemarkName,
                                     cc.ParentId
                                     //cc.Avatar
                                 };
                if (m.PraisesCount > 0)
                    m.PraiseUsers = from cc in _PraiseRepository.FindAsIQueryable(cc => cc.ContentGuid == m.ContentGuid && cc.IsFriend).Take(100)
                                    .Select(cc => new { cc.CreateID, cc.CreateDate, cc.UserInfo.Avatar })
                                    join ff in _friendRepository.FindAsIQueryable(ff => ff.CreateID == UserContext.Current.UserId)
                                    .Select(ff => new { ff.FriendRemarkName, ff.FriendUserId })
                                    on cc.CreateID equals ff.FriendUserId
                                    orderby cc.CreateDate ascending
                                    select new
                                    {
                                        UserId = cc.CreateID,
                                        //cc.CreateDate,
                                        NickName = ff.FriendRemarkName
                                        //cc.Avatar
                                    };
                if (m.ParentGuid.IsNotNullOrEmpty())
                    m.Parent = _repository.FindAsIQueryable(mm => mm.ContentGuid == m.ParentGuid).Select(mm => new
                    {
                        mm.ContentGuid,
                        mm.Contents,
                        mm.CreateDate,
                        Pic = mm.ContentFiles.Select(mmm => mmm.UploadPath).FirstOrDefault()
                    }).FirstOrDefault();
            });
            return result;
        }

        public object FriendPics(DateTime date)
        {
            var result = _FileRepository.FindAsIQueryable(m => m.CreateDate < date && m.CreateID == UserContext.Current.UserId)
                                  .Take(10).Select(m => new { Date = m.CreateDate.ToString("MM"), m.CreateDate, m.UploadPath }).ToList();
            if (result?.Count > 0)
            {
                var minDate = result.Min(m => m.CreateDate);
                var data = result.GroupBy(m => m.Date).Select(m => new { m.Key, Pics = m.Select(mm => mm.UploadPath) });
                return new { minDate = minDate, photos = data };
            }
            return null;
        }
        #endregion

        #region 收藏内容
        public IList<AppContent> CollectContents(DateTime searchDate)
        {
            var currentUserId = UserContext.Current.UserId;
            var result = (from r in _collectRepository.FindAsIQueryable(m => m.CreateID == currentUserId && m.CreateDate < searchDate)
                          join c in _repository.FindAsIQueryable(m => m.IsPublic) on r.ContentGuid equals c.ContentGuid
                          select new AppContent
                          {
                              guid = c.ContentGuid,
                              parentGuid = c.ParentGuid,
                              contents = c.Contents,
                              createDate = r.CreateDate,
                              createID = c.CreateID,
                              pics = c.ContentFiles.Select(m => new { path = m.UploadPath, length = m.Length })
                          }).OrderByDescending(m => m.createDate).Take(5).ToList();
            result.ForEach(x =>
            {
                if (x.parentGuid != null)
                {
                    x.parent = _repository.FindAsIQueryable(m => m.ContentGuid == x.parentGuid).Select(m => new
                    {
                        pics = m.ContentFiles.Select(mm => new { path = mm.UploadPath, length = mm.Length }),
                        m.Contents,
                        createID = m.CreateID,
                        guid = m.ContentGuid,
                        contentUser = new ContentUser
                        {
                            nickName = m.UserInfo.NickName,
                            avatar = m.UserInfo.Avatar,
                            userId = m.CreateID,
                            remark = m.UserInfo.Remark
                        }
                    }).FirstOrDefault();
                }
            });
            return result;
        }
        #endregion

        #region 统计内容

        public (int, int, int) Statistic()
        {
            var userId = UserContext.Current.UserId;
            var contentCount = _repository.FindAsIQueryable(m => m.CreateID == userId && m.IsPublic).Count();
            var xinZanCount = _UserRepository.DbContext.Set<User_XinPraise>().Where(m => m.UserId == userId).First()?.Quantity;
            var interactionCount = _XinPraiseRepository.FindAsIQueryable(m => m.CreateID == userId).Count()
                                    + _PraiseRepository.FindAsIQueryable(m => m.CreateID == userId).Count()
                                    + _CommentRepository.FindAsIQueryable(m => m.CreateID == userId).Count();
            return (contentCount, xinZanCount.ToInt(), interactionCount);
        }
        #endregion

    }
}
