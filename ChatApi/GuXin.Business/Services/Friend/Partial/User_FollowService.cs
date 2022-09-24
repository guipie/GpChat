/*
 *所有关于User_Follow类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_FollowService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using GuXin.Entity.DomainModels;
using System.Linq;
using GuXin.Core.Utilities;
using GuXin.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.Business.IRepositories;
using System;
using GuXin.Core.ManageUser;

namespace GuXin.Business.Services
{
    public partial class User_FollowService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_FollowRepository _repository;//访问数据库
        private readonly IUserRepository _UserRepository;//访问数据库
        private readonly IUser_Follow_SettingRepository _Follow_SettingRepository;
        [ActivatorUtilitiesConstructor]
        public User_FollowService(
            IUser_FollowRepository dbRepository, IUserRepository sys_UserRepository, IUser_Follow_SettingRepository user_Follow_SettingRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _UserRepository = sys_UserRepository;
            _Follow_SettingRepository = user_Follow_SettingRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public User_Follow_Setting GetCurrentSettings()
        {
            var result = _Follow_SettingRepository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId).FirstOrDefault();
            if (result == null)
            {
                result = new User_Follow_Setting() { NotDisturb = false, Top = false, Disable = false, LastNoticeTime = DateTime.Now.AddYears(-1) }.SetCreateDefaultVal();
                _Follow_SettingRepository.Add(result, true);
            }
            return result;
        }
        public int IsNotDisturb(bool notDisturb)
        {
            var settings = _Follow_SettingRepository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Follow_Setting() { NotDisturb = notDisturb }.SetCreateDefaultVal();
                _Follow_SettingRepository.Add(settings, true);
            }
            else
            {
                settings.NotDisturb = notDisturb;
                settings.ModifyDate = DateTime.Now;
                _Follow_SettingRepository.Update(settings.SetModifyDefaultVal(), m => new { m.NotDisturb, m.ModifyDate }, true);
            }
            return settings.Id;
        }

        public int IsTop(bool top)
        {
            var settings = _Follow_SettingRepository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Follow_Setting() { Top = top }.SetCreateDefaultVal();
                _Follow_SettingRepository.Add(settings, true);
            }
            else
            {
                settings.Top = top;
                settings.ModifyDate = DateTime.Now;
                _Follow_SettingRepository.Update(settings.SetModifyDefaultVal(), m => new { m.Top, m.ModifyDate }, true);
            }
            return settings.Id;
        }
        public int IsDisable(bool disable)
        {
            var settings = _Follow_SettingRepository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Follow_Setting() { Disable = disable }.SetCreateDefaultVal();
                _Follow_SettingRepository.Add(settings, true);
            }
            else
            {
                settings.Disable = disable;
                settings.ModifyDate = DateTime.Now;
                _Follow_SettingRepository.Update(settings.SetModifyDefaultVal(), m => new { m.Disable, m.ModifyDate }, true);
            }
            return settings.Id;
        }


        public int ClearInteractNotices()
        {
            var settings = _Follow_SettingRepository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Follow_Setting() { LastNoticeTime = DateTime.Now }.SetCreateDefaultVal();
                _Follow_SettingRepository.Add(settings, true);
            }
            else
            {
                settings.LastNoticeTime = DateTime.Now;
                settings.ModifyDate = DateTime.Now;
                _Follow_SettingRepository.Update(settings.SetModifyDefaultVal(), m => new { m.LastNoticeTime, m.ModifyDate }, true);
            }
            return settings.Id;
        }
        public WebResponseContent FollowOrUnFollow(int userId)
        {
            if (_repository.Exists(m => m.CreateID == UserContext.Current.UserId && m.FollowUserId == userId))
            {
                _repository.Delete(m => m.CreateID == UserContext.Current.UserId && m.FollowUserId == userId, true);
                return WebResponseContent.Instance.Info("已取消关注", -1);
            }
            else
            {
                _repository.Add(new User_Follow() { FollowUserId = userId }.SetCreateDefaultVal(), true);
                return WebResponseContent.Instance.Info("关注成功", 1);
            }
        }
        public IQueryable<object> GetFollowUsers(DateTime date)
        {
            return from f in _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId).Take(10).OrderByDescending(m => m.CreateDate)
                   join u in _UserRepository.FindAsIQueryable(m => m.Enable == Entity.AppEnum.UserStatus.Enable)
                   on f.FollowUserId equals u.CreateID
                   select new
                   {
                       UserId = f.FollowUserId,
                       u.Avatar,
                       u.NickName
                   };
        }
    }
}
