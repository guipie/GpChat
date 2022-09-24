/*
 *所有关于Content_Comment类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Content_CommentService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using System.Collections.Generic;
using GuXin.Entity.AppDto.Output;
using System.Threading.Tasks;
using GuXin.ImCore;

namespace GuXin.Business.Services
{
    public partial class Content_CommentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_CommentRepository _repository;
        private readonly IContent_Comment_PraiseRepository _Comment_PraiseRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IUser_FriendRepository _FriendRepository;
        private readonly IUserRepository _userRepository;

        [ActivatorUtilitiesConstructor]
        public Content_CommentService(
            IContent_CommentRepository dbRepository,
            IHttpContextAccessor httpContextAccessor,
            IUser_FriendRepository user_FriendRepository,
            IUserRepository userRepository,
            IContentRepository contentRepository,
            IContent_Comment_PraiseRepository content_Comment_PraiseRepository
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _contentRepository = contentRepository;
            _FriendRepository = user_FriendRepository;
            _userRepository = userRepository;
            _Comment_PraiseRepository = content_Comment_PraiseRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        public IList<ContentComent> GetComments(Guid contentGuid, DateTime searchDate)
        {
            var list = _repository.FindAsIQueryable(m => m.CreateDate <= searchDate && m.ContentGuid == contentGuid && m.IsPublic)
                             .OrderByDescending(m => m.CreateDate).Take(20)
                             .Select(m => new ContentComent
                             {
                                 id = m.Id,
                                 parentId = m.ParentId,
                                 comments = m.Contents,
                                 praiseCount = m.PraiseCount,
                                 createDate = m.CreateDate,
                                 createId = m.CreateID,
                                 commentUser = new CommentUser { nickName = m.UserInfo.NickName, avatar = m.UserInfo.Avatar, userId = m.UserInfo.User_Id }
                             }).ToList();
            list.ForEach(m =>
            {
                if (m.parentId > 0)
                    m.parentcommentUser = _repository.FindAsIQueryable(x => x.Id == m.parentId)
                    .Select(x => new CommentUser()
                    {
                        nickName = x.UserInfo.NickName,
                        avatar = x.UserInfo.Avatar,
                        userId = x.UserInfo.User_Id
                    }).First();
                m.praised = _Comment_PraiseRepository.Exists(x => x.CommentId == m.id && x.CreateID == UserContext.Current.UserId);
            });
            return list;
        }
        public IQueryable<object> GetFriendComments(Guid contentGuid, DateTime searchDate)
        {
            return (from c in _repository.FindAsIQueryable(m => m.CreateDate <= searchDate && m.ContentGuid == contentGuid && m.IsPublic)
                    join f in _FriendRepository.FindAsIQueryable(m => m.FriendUserId == UserContext.Current.UserId) on c.CreateID equals f.CreateID
                    select new
                    {
                        c.Contents,
                        c.ParentId,
                        NickName = f.FriendRemarkName,
                        UserId = f.FriendUserId,
                        c.UserInfo.Avatar
                    }).Take(20);
        }
        public Content_Comment PostComment(Guid contentGuid, string comments, int? parentGuid)
        {
            Content_Comment cc = new Content_Comment()
            {
                ContentGuid = contentGuid,
                Contents = comments,
                ParentId = parentGuid,
                IsPublic = true
            }.SetCreateDefaultVal();
            _repository.Add(cc);
            var content = _contentRepository.FindAsIQueryable(m => m.ContentGuid == contentGuid).AsTracking().FirstOrDefault();
            content.CommentCount += 1;
            var result = _repository.SaveChanges();
            //推送通知
            var setting = _repository.DbContext.Set<User_Interact_Setting>().FirstOrDefault(m => m.CreateID == content.CreateID);
            if (setting == null || !setting.Disable)
                Task.Factory.StartNew(() => { ImHelper.SendNotice(UserContext.Current.UserId, content.CreateID, UserContext.Current.NickName + "评论了您", ChatType.Comment); });
            return cc;
        }
        public Content_Comment PutComment(int commentsId, string comments)
        {
            var comment = _repository.FindAsIQueryable(m => m.Id == commentsId).AsTracking().FirstOrDefault();
            comment.Contents = comments;
            var result = _repository.SaveChanges();
            return comment;
        }
        public bool DelComment(int commentsId)
        {
            var comment = _repository.DeleteWithKeys(new object[1] { commentsId });
            _Comment_PraiseRepository.Delete(m => m.CommentId == commentsId);
            return _repository.SaveChanges() > 0;
        }
        public int PraiseComment(int Id)
        {
            int result = 0;
            var comment = _repository.FindAsIQueryable(m => m.Id == Id).AsTracking().FirstOrDefault();
            var commentPraise = _Comment_PraiseRepository.FindAsIQueryable(m => m.CommentId == Id && m.CreateID == UserContext.Current.UserId).FirstOrDefault();
            if (commentPraise?.CreateID == UserContext.Current.UserId)
            {
                comment.PraiseCount--;
                result = -1;
                _Comment_PraiseRepository.DeleteWithKeys(new object[1] { commentPraise.Id });
            }
            else
            {
                comment.PraiseCount++;
                result = 1;
                _Comment_PraiseRepository.Add(new Content_Comment_Praise() { CommentId = Id }.SetCreateDefaultVal());
            }
            _repository.SaveChanges();
            return result;
        }
        public Content_Comment PostFriendComment(Guid contentGuid, string comments, int? parentGuid)
        {
            Content_Comment cc = new Content_Comment()
            {
                ContentGuid = contentGuid,
                Contents = comments,
                ParentId = parentGuid,
                IsFriend = true
            }.SetCreateDefaultVal();
            _repository.Add(cc, true);
            return cc;
        }
        public WebResponseContent PutCommentPraise(int commentId)
        {
            var entity = _repository.FindAsIQueryable(m => m.Id == commentId).AsTracking().FirstOrDefault();
            entity.PraiseCount += 1;
            _repository.SaveChanges();
            return WebResponseContent.Instance.OK("点赞成功");
        }
    }
}
