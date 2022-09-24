/*
 *所有关于User_Interact_Setting类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_Interact_SettingService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using GuXin.Entity.AppDto.Output;
using System.Collections.Generic;
using GuXin.Business.IServices;

namespace GuXin.Business.Services
{
    /// <summary>
    /// 互动通知相关
    /// </summary>
    public partial class User_Interact_NoticeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_Interact_SettingRepository _repository;//访问数据库
        private readonly ICacheService _cacheService;
        private readonly IContent_FileService _FileService;
        [ActivatorUtilitiesConstructor]
        public User_Interact_NoticeService(
            IUser_Interact_SettingRepository dbRepository,
            ICacheService cacheService, IContent_FileService content_File,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _cacheService = cacheService;
            _FileService = content_File;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public User_Interact_Setting GetCurrentSettings()
        {
            var result = _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId).FirstOrDefault();
            if (result == null)
            {
                result = new User_Interact_Setting() { NotDisturb = false, Top = false, Disable = false }.SetCreateDefaultVal();
                _repository.Add(result, true);
            }
            return result;
        }
        public int IsNotDisturb(bool notDisturb)
        {
            var settings = _repository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Interact_Setting() { NotDisturb = notDisturb }.SetCreateDefaultVal();
                _repository.Add(settings, true);
            }
            else
            {
                settings.NotDisturb = notDisturb;
                settings.ModifyDate = DateTime.Now;
                _repository.Update(settings.SetModifyDefaultVal(), m => new { m.NotDisturb, m.ModifyDate }, true);
            }
            return settings.Id;
        }

        public int IsTop(bool top)
        {
            var settings = _repository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Interact_Setting() { Top = top }.SetCreateDefaultVal();
                _repository.Add(settings, true);
            }
            else
            {
                settings.Top = top;
                settings.ModifyDate = DateTime.Now;
                _repository.Update(settings.SetModifyDefaultVal(), m => new { m.Top, m.ModifyDate }, true);
            }
            return settings.Id;
        }
        public int IsDisable(bool disable)
        {
            var settings = _repository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Interact_Setting() { Disable = disable }.SetCreateDefaultVal();
                _repository.Add(settings, true);
            }
            else
            {
                settings.Disable = disable;
                settings.ModifyDate = DateTime.Now;
                _repository.Update(settings.SetModifyDefaultVal(), m => new { m.Disable, m.ModifyDate }, true);
            }
            return settings.Id;
        }


        public int ClearInteractNotices()
        {
            var settings = _repository.FindFirst(m => m.CreateID == UserContext.Current.UserId);
            if (settings == null)
            {
                settings = new User_Interact_Setting() { LastNoticeTime = DateTime.Now }.SetCreateDefaultVal();
                _repository.Add(settings, true);
            }
            else
            {
                settings.LastNoticeTime = DateTime.Now;
                settings.ModifyDate = DateTime.Now;
                _repository.Update(settings.SetModifyDefaultVal(), m => new { m.LastNoticeTime, m.ModifyDate }, true);
            }
            return settings.Id;
        }
        public IList<InteractNoticeDto> GetInteractNotices(DateTime searchDate)
        {
            var userId = UserContext.Current.UserId;
            var result = repository.DbContext.Database.SqlQuery<InteractNoticeDto>($@"select t1.*,t2.Contents,t2.PicCount,t3.Avatar,t3.NickName from(
                                select * from (SELECT TOP (5) ContentGuid,CreateDate,CreateID,'点赞了' Type  FROM [dbo].[Content_Praise] where CreateDate<'{searchDate}' and IsPublic=1 order by CreateDate Desc)a
                                UNION ALL
                                select * from (SELECT TOP (5) ContentGuid,CreateDate,CreateID,'新赞了' Type  FROM [dbo].[Content_XinPraise] where CreateDate<'{searchDate}'  order by CreateDate Desc)b
                                UNION ALL
                                select * from (SELECT TOP (5) ContentGuid,CreateDate,CreateID,'评论了' Type  FROM [dbo].[Content_Comment] where CreateDate<'{searchDate}'  and IsPublic=1 order by CreateDate Desc)c
                                ) t1
                                inner join  [dbo].[Content] t2 on t1.ContentGuid=t2.ContentGuid 
                                left join dbo.Sys_User t3 on t1.CreateID=t3.User_Id
                                where t2.CreateId={userId} order by t1.CreateDate desc");
            result.ForEach(x =>
            {
                if (x.PicCount > 0)
                    x.Pics = _FileService.GetFiles(x.ContentGuid).Select(m => m.UploadPath).ToArray();
                if (x.Type == "点赞了")
                    x.PraiseCount = _repository.DbContext.Set<Content_Praise>().Count(m => m.ContentGuid == x.ContentGuid && m.CreateDate <= x.CreateDate);
            });
            return result;
        }
    }
}
