/*
 *所有关于Content类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*ContentService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using System.Collections.Generic;
using GuXin.Core.Enums;
using GuXin.Entity.AppDto.Input;
using GuXin.Business.IServices;
using GuXin.Entity.AppDto.Output;
using GuXin.Core.ManageUser;
using GuXin.Entity.AppDto.Common;
using System.Threading.Tasks;
using GuXin.ImCore;

namespace GuXin.Business.Services
{
    public partial class ContentService
    {
        public WebResponseContent Del(Guid guid)
        {
            if (!repository.Exists(m => m.ContentGuid == guid && m.CreateID == UserContext.Current.UserId))
                return WebResponseContent.Instance.Error("您没有权限删除此内容");
            return _repository.DbContextBeginTransaction(() =>
            {

                _repository.Delete(m => m.ContentGuid == guid);
                _FileRepository.Delete(m => m.ContentGuid == guid);
                _CommentRepository.Delete(m => m.ContentGuid == guid);
                _PraiseRepository.Delete(m => m.ContentGuid == guid);
                _XinPraiseRepository.Delete(m => m.ContentGuid == guid);
                return WebResponseContent.Instance.Info(_repository.SaveChanges(), "删除成功");
            });
        }

        public async Task<List<AppContent>> GetAsync(int userId, DateTime searchDate)
        {
            return await ContentsAsync<float>(m => m.CreateID == userId && m.CreateDate < searchDate);
        }
        public async Task<List<AppContent>> GetTopAsync(int userId, DateTime searchDate)
        {
            var currentUserId = UserContext.Current.UserId;
            TopSettingDto topSettingDto = _settingService.GetTopSettings();
            Expression<Func<Content, float>> desc = m => m.ShareCount;
            return await ContentsAsync(m => m.CreateID == userId && m.ShareCount >= 10 && m.XinPraiseCount >= 10 && m.CreateDate <= searchDate, desc);
        }
        public WebResponseContent Post(ContentInput inputDto)
        {
            Content entity = new Content()
            {
                ParentGuid = inputDto.ParentGuid,
                Contents = inputDto.Content,
                IsPublic = inputDto.IsPublic,
                IsFriend = inputDto.IsFriend,
                VisibleDays = inputDto.VisibleDays
            }.SetCreateDefaultVal();
            var result = _repository.DbContextBeginTransaction(() =>
             {
                 entity.PicCount = inputDto.Files.Count;
                 _repository.Add(entity);
                 inputDto.Files.ForEach(m => m.ContentGuid = entity.ContentGuid);
                 inputDto.Files.ForEach(m =>
                 {
                     var file = new Content_File().SetCreateDefaultVal();
                     m.MapValueToEntity(file);
                     file.ContentGuid = entity.ContentGuid;
                     file.IsFriend = inputDto.IsFriend;
                     file.IsPublic = inputDto.IsPublic;
                     _FileRepository.Add(file);
                 });
                 return WebResponseContent.Instance.Info(_repository.SaveChanges(), "发布成功");
             });
            if (result.Status) //推送通知 
            {
                var userId = UserContext.Current.UserId;
                var nickName = UserContext.Current.NickName;
                //var fr = Core.Utilities.HttpContext.Current.GetService<IUser_FollowRepository>();
                //var ur = Core.Utilities.HttpContext.Current.GetService<IUserRepository>();
                var pushUserIds = _followRepository.FindAsIQueryable(m => m.FollowUserId == userId).Select(m => m.CreateID).ToList();
                Task.Factory.StartNew(() =>
                {
                    pushUserIds.ForEach(pushId =>
                     {
                         ImHelper.SendNotice(userId, pushId, nickName + "发布了新内容", ChatType.Follow);
                     });
                });
            }
            return result;
        }
        public ContentInput EditDetail(Guid guid)
        {
            var entity = _repository.FindAsIQueryable(m => m.ContentGuid == guid).FirstOrDefault();
            return new ContentInput()
            {
                Guid = entity.ContentGuid,
                ParentGuid = entity.ParentGuid,
                IsPublic = entity.IsPublic,
                VisibleDays = entity.VisibleDays,
                Content = entity.Contents,
                Files = _FileRepository.FindAsIQueryable(m => m.ContentGuid == guid).Select(m => new ContentFileInput()
                {
                    UploadPath = m.UploadPath,
                    ContentGuid = m.ContentGuid,
                    Guid = m.Guid
                }).ToList()
            };
        }
        public bool Put(ContentInput inputDto)
        {
            var entity = _repository.FindAsIQueryable(m => m.ContentGuid == inputDto.Guid).AsTracking().FirstOrDefault();
            var addFileCount = inputDto.Files == null ? 0 : inputDto.Files.Count;
            var delFileCount = inputDto.DelFiles == null ? 0 : inputDto.DelFiles.Length;
            entity.PicCount = entity.PicCount + addFileCount - delFileCount;
            entity.Contents = inputDto.Content;
            inputDto.Files.ForEach(m =>
            {
                var file = new Content_File().SetCreateDefaultVal();
                m.MapValueToEntity(file);
                file.ContentGuid = entity.ContentGuid;
                file.IsFriend = inputDto.IsFriend;
                file.IsPublic = inputDto.IsPublic;
                _FileRepository.Add(file);
            });
            if (inputDto.DelFiles?.Length > 0)
                _FileRepository.Delete(m => inputDto.DelFiles.Contains(m.Guid));
            return _repository.SaveChanges() > 0;
        }

    }
}
