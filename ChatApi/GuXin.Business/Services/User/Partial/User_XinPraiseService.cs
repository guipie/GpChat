/*
 *所有关于User_XinPraise类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_XinPraiseService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using GuXin.Entity.AppEnum;
using GuXin.Core.ManageUser;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using GuXin.Core.Services;
using System;
using GuXin.Entity.AppDto.Output;
using GuXin.Core.CacheManager;

namespace GuXin.Business.Services
{
    public partial class User_XinPraiseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_XinPraiseRepository _Content_XinPraiseRepository;
        private readonly IUser_XinPraiseRepository _repository;//访问数据库
        private readonly IUser_XinPraise_RecordRepository _XinPraise_RecordRepository;//访问数据库
        private readonly IUserRepository _userRepository;
        private readonly IContentRepository _contentRepository;
        private readonly ICacheService _cacheService;
        [ActivatorUtilitiesConstructor]
        public User_XinPraiseService(
            IUser_XinPraiseRepository dbRepository, IUser_XinPraise_RecordRepository user_XinPraise_Record,
            IContent_XinPraiseRepository content_XinPraiseRepository,
            IUserRepository userRepository,
            IContentRepository contentRepository,
            IHttpContextAccessor httpContextAccessor,
            ICacheService cacheService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _XinPraise_RecordRepository = user_XinPraise_Record;
            _Content_XinPraiseRepository = content_XinPraiseRepository;
            _userRepository = userRepository;
            _contentRepository = contentRepository;
            _cacheService = cacheService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public User_XinPraise Get()
        {
            return _repository.FindFirst(m => m.UserId == UserContext.Current.UserId);
        }
        public UserXinZanInfo GetInfo()
        {
            var entity = Get();
            var inviteUserIds = _XinPraise_RecordRepository.FindAsIQueryable(m => m.UserId == UserContext.Current.UserId && m.PraiseType == XinPraiseType.Invite).Select(m => m.CreateID).ToList();
            //今日使用新赞数
            int usedCount = _Content_XinPraiseRepository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId && m.CreateDate.DayOfYear == DateTime.Now.DayOfYear).Count();
            var currentXinZanCount = UserContext.Current.UserInfo.XinZan;
            int nextUsedCount = currentXinZanCount[currentXinZanCount.Count - 1 - usedCount < 0 ? 0 : currentXinZanCount.Count - 1 - usedCount];
            UserXinZanInfo userXinZanInfo = new()
            {
                quantity = entity.Quantity,
                todayUsed = usedCount,
                nextUsed = nextUsedCount,
                invitedUsers = _userRepository.FindAsIQueryable(m => inviteUserIds.Contains(m.User_Id)).Select(m => new { userId = m.User_Id, m.NickName, m.Avatar }),
                invitedCode = UserContext.Current.UserName
            };
            return userXinZanInfo;
        }
        /// <summary>
        /// 累计新赞
        /// </summary>
        /// <param name="xinPraiseType"></param>
        /// <returns></returns>
        public async Task<int> PostAsync(XinPraiseType xinPraiseType, int userId)
        {
            userId = userId == 0 ? UserContext.Current.UserId : userId;

            switch (xinPraiseType)
            {
                case XinPraiseType.Login:
                    if (_XinPraise_RecordRepository.Exists(m => m.CreateID == userId && m.CreateDate.HasValue && m.CreateDate.Value.Date == DateTime.Now.Date))
                        return 0;
                    break;
                case XinPraiseType.Invite:
                    break;
                default:
                    Logger.Error(Core.Enums.LoggerType.XinZan, "新赞累计出错", new { xinPraiseType, userId }.ToJson());
                    return 0;
            }
            var entity = await _repository.FindAsIQueryable(m => m.UserId == userId).AsTracking().FirstOrDefaultAsync();
            if (entity?.UserId > 0)
            {
                entity.Quantity++;
                entity.ModifyDate = DateTime.Now;
            }
            else
                _repository.Add(new User_XinPraise()
                {
                    UserId = userId,
                    Quantity = 1
                }.SetModifyDefaultVal());
            _XinPraise_RecordRepository.Add(new User_XinPraise_Record()
            {
                Quantity = 1,
                PraiseType = xinPraiseType,
                UserId = userId
            }.SetCreateDefaultVal());
            return _repository.SaveChanges();
        }

    }
}
