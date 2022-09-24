/*
 *所有关于User_Sys_Notice类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_Sys_NoticeService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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

namespace GuXin.Business.Services
{
    public partial class User_Sys_NoticeService
    {
        private readonly string Notice_Cache_Key = UserContext.Current.UserId + "_Notice";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_Sys_NoticeRepository _repository;//访问数据库
        private readonly IUser_Sys_Notice_SettingRepository _SettingRepository;
        private readonly ICacheService _cacheService;
        [ActivatorUtilitiesConstructor]
        public User_Sys_NoticeService(
            IUser_Sys_NoticeRepository dbRepository, IUser_Sys_Notice_SettingRepository user_Sys_Notice_SettingRepository,
            IHttpContextAccessor httpContextAccessor, ICacheService cache
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _SettingRepository = user_Sys_Notice_SettingRepository;
            _cacheService = cache;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }


        public User_Sys_Notice_Setting Get()
        {
            var entity = _SettingRepository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (entity == null)
            {
                entity = new User_Sys_Notice_Setting()
                {
                    Top = false,
                    NotDisturb = false,
                    LastNoticeTime = DateTime.Now.AddDays(-1)
                }.SetCreateDefaultVal();
                _SettingRepository.Add(entity, true);
            }
            return entity;
        }

        public void NoticeClear(int settingId)
        {
            _SettingRepository.Update(new User_Sys_Notice_Setting() { LastNoticeTime = DateTime.Now, Id = settingId }, m => m.LastNoticeTime, true);
        }

        public IQueryable<object> Notices(DateTime searchDate)
        {
            var entity = Get();
            var lastDate = entity == null ? DateTime.Now.AddYears(-1) : entity.LastNoticeTime;
            return _repository.FindAsIQueryable(m => m.CreateDate <= searchDate && m.CreateDate > lastDate).OrderByDescending(m => m.CreateDate).Take(5);
        }

        public bool NoticeSetting(bool? isTop, bool? isNotDisturb)
        {
            var entity = _SettingRepository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId).AsTracking().FirstOrDefault();
            if (isTop != null) entity.Top = (bool)isTop;
            if (isNotDisturb != null) entity.NotDisturb = (bool)isNotDisturb;
            entity.ModifyDate = DateTime.Now;
            return _SettingRepository.SaveChanges() > 0;
        }
    }
}
