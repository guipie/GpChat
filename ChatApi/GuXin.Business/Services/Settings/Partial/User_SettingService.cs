/*
 *所有关于User_Setting类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_SettingService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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

namespace GuXin.Business.Services
{
    /// <summary>
    /// 当前用户相关配置
    /// </summary>
    public partial class User_SettingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_SettingRepository _repository;//访问数据库

        [ActivatorUtilitiesConstructor]
        public User_SettingService(
            IUser_SettingRepository dbRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        public User_Setting GetCurrentSetting()
        {
            var entity = _repository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (entity == null)
                _repository.Add(new User_Setting() { HalfYearVisible = false, ReplyByFollow = false }.SetCreateDefaultVal(), true);
            return entity;
        }

        public bool CurrentSetting(bool? halfYear, bool? followReply)
        {
            var entity = _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId).AsTracking().FirstOrDefault();
            if (halfYear != null) entity.HalfYearVisible = (bool)halfYear;
            if (followReply != null) entity.ReplyByFollow = (bool)followReply;
            entity.ModifyDate = DateTime.Now;
            return _repository.SaveChanges() > 0;
        }

    }
}
