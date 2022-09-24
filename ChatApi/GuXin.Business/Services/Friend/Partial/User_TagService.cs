/*
 *所有关于User_Tag类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_TagService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using GuXin.Entity.DomainModels;
using System.Linq;
using GuXin.Core.Utilities;
using GuXin.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.Business.IRepositories;
using GuXin.Entity.AppDto.Input;
using GuXin.Core.ManageUser;
using System;

namespace GuXin.Business.Services
{
    public partial class User_TagService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_TagRepository _repository;//访问数据库
        private readonly IUser_FriendRepository _FriendRepository;
        private readonly IUser_Tag_MemberRepository _tagMemberRepository;
        private readonly IUserRepository _UserRepository;
        [ActivatorUtilitiesConstructor]
        public User_TagService(
            IUser_TagRepository dbRepository,
            IUser_Tag_MemberRepository tag_MemberRepository,
            IUser_FriendRepository user_FriendRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _tagMemberRepository = tag_MemberRepository;
            _FriendRepository = user_FriendRepository;
            _UserRepository = userRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        private WebResponseContent Add(TagDto tag)
        {
            var currentUserId = UserContext.Current.UserId;
            var entity = _repository.FindFirst(m => m.Name == tag.tagName && m.CreateID == currentUserId);
            if (entity != null) return WebResponseContent.Instance.Error("此标签已经存在");
            return _repository.DbContextBeginTransaction(() =>
            {
                entity = new User_Tag() { Name = tag.tagName, MemberCount = tag.memberUserIds.Length }.SetCreateDefaultVal();
                _repository.Add(entity, true);
                var members = _FriendRepository.FindAsIQueryable(m => m.CreateID == currentUserId && tag.memberUserIds.Contains(m.FriendUserId))
                            .Select(m => new User_Tag_Member()
                            {
                                TagId = entity.TagId,
                                UserId = m.FriendUserId,
                                TagName = tag.tagName,
                                UserNickName = m.FriendRemarkName,
                                CreateID = currentUserId,
                                CreateDate = DateTime.Now,
                                Creator = UserContext.Current.NickName
                            });
                if (members.Count() == 0) return WebResponseContent.Instance.Error("标签成员未在好友列表里");
                _tagMemberRepository.AddRange(members);
                return WebResponseContent.Instance.Info(_repository.SaveChanges());
            });
        }
        private WebResponseContent Update(TagDto tag)
        {
            if (_repository.Exists(m => m.TagId != tag.tagId && m.Name == tag.tagName && m.CreateID == UserContext.Current.UserId))
                return WebResponseContent.Instance.Error("此标签已经存在");

            var currentUserId = UserContext.Current.UserId;
            return _repository.DbContextBeginTransaction(() =>
            {
                var entity = _repository.FindAsIQueryable(m => m.TagId == tag.tagId && m.CreateID == UserContext.Current.UserId).AsTracking().FirstOrDefault();
                var sourceMembers = _tagMemberRepository.FindAsIQueryable(m => m.TagId == tag.tagId).Select(m => m.UserId).ToArray();
                var delMebers = sourceMembers.Except(tag.memberUserIds); //过滤相同ID
                var addMebers = tag.memberUserIds.Except(sourceMembers); //过滤相同ID
                entity.Name = tag.tagName;
                entity.MemberCount = entity.MemberCount + addMebers.Count() - delMebers.Count();
                entity.SetModifyDefaultVal();
                _tagMemberRepository.AddRange(_FriendRepository.FindAsIQueryable(m => m.CreateID == currentUserId && addMebers.Contains(m.FriendUserId))
                            .Select(m => new User_Tag_Member()
                            {
                                TagId = entity.TagId,
                                UserId = m.FriendUserId,
                                TagName = tag.tagName,
                                UserNickName = m.FriendRemarkName,
                                CreateID = currentUserId,
                                CreateDate = DateTime.Now,
                                Creator = UserContext.Current.NickName
                            }));
                _tagMemberRepository.Delete(m => delMebers.Contains(m.UserId) && m.TagId == tag.tagId);
                return WebResponseContent.Instance.Info(_repository.SaveChanges());
            });
        }
        public WebResponseContent Post(TagDto tag)
        {
            if (tag.tagId > 0) return Update(tag);
            else return Add(tag);
        }
        public IQueryable<object> GetTags()
        {
            return _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId).Select(m => new
            {
                m.Name,
                m.TagId,
                m.MemberCount,
                Members = m.User_Tag_Member.Take(5).Select(x => x.UserNickName)
            });
        }
        public IQueryable<object> GetMembers(int tagId)
        {
            return _tagMemberRepository.FindAsIQueryable(m => m.TagId == tagId)
                .Join(_UserRepository.FindAsIQueryable(m => true), m => m.UserId, n => n.User_Id, (m, n) => new
                {
                    n.UserName,
                    FriendUserId = m.UserId,
                    FriendRemarkName = m.UserNickName,
                    n.Avatar
                });
        }
    }
}
