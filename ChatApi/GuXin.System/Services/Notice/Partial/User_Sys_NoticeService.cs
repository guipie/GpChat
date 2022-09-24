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
using GuXin.System.IRepositories;
using GuXin.Core.ManageUser;
using System;
using GuXin.Core.CacheManager;
using System.Threading.Tasks;

namespace GuXin.System.Services
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

        public override WebResponseContent Add(SaveModel saveDataModel)
        {
            var result = base.Add(saveDataModel);
            var myUserId = UserContext.Current.UserId;
            if (result.Status)
                Task.Factory.StartNew(() =>
                {
                    var keys = RedisHelper.Keys("UID*");
                    foreach (var userId in keys)
                        ImHelper.SendNotice(myUserId, userId.Replace("UID", "").ToInt(), saveDataModel.MainData.GetValue("Title").ToString(), ImCore.ChatType.Notice, true);
                });
            return result;
        }
    }
}
