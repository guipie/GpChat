/*
 *所有关于User_Friend类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_FriendService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using System;
using GuXin.Entity.AppDto.Output;
using System.Collections.Generic;
using GuXin.ImCore;
using System.Threading.Tasks;

namespace GuXin.Business.Services
{
    public partial class User_FriendService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_FriendRepository _repository;//访问数据库
        private readonly IUserRepository _userRepository;
        private readonly IContent_FileRepository _fileRepository;
        private readonly IUser_Tag_MemberRepository _tag_MemberRepository;

        [ActivatorUtilitiesConstructor]
        public User_FriendService(
            IUser_FriendRepository dbRepository, IUserRepository sys_UserRepository, IContent_FileRepository content_FileRepository, IUser_Tag_MemberRepository tag_MemberRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _fileRepository = content_FileRepository;
            _userRepository = sys_UserRepository;
            _tag_MemberRepository = tag_MemberRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }


        public WebResponseContent AcceptFriend(int friendUserId)
        {
            var userInfo = _userRepository.FindFirst(m => m.User_Id == friendUserId && m.Enable == UserStatus.Enable);
            if (userInfo == null) return WebResponseContent.Instance.Error("此好友已经注销");

            var friendEntity = _repository.FindAsIQueryable(m => m.CreateID == friendUserId && m.FriendUserId == UserContext.Current.UserId).AsTracking().FirstOrDefault();
            if (friendEntity == null) return WebResponseContent.Instance.Error("好友请求已过期");
            friendEntity.Status = FriendStatus.Friend;
            friendEntity.ModifyDate = DateTime.Now;

            var myEntity = _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId && m.FriendUserId == friendUserId).AsTracking().FirstOrDefault();
            if (myEntity == null)
            {
                myEntity = new User_Friend() { FriendUserId = friendUserId, Status = FriendStatus.Friend, FriendRemarkName = userInfo.NickName };
                myEntity.SetCreateDefaultVal();
                _repository.Add(myEntity);
            }
            else
            {
                myEntity.Status = FriendStatus.Friend;
                myEntity.ModifyDate = DateTime.Now;
            }
            var result = _repository.SaveChanges() > 0;
            if (result)
                Task.Factory.StartNew(() =>
                {
                    ImHelper.SendNotice(UserContext.Current.UserId, friendUserId, "好友请求", ChatType.FriendAccept);
                });

            return WebResponseContent.Instance.Info("已经是您的好友了", result);
        }

        public WebResponseContent AddFriend(int friendUserId, string nickName, string resean)
        {
            var userInfo = _userRepository.FindFirst(m => m.User_Id == friendUserId);
            if (userInfo == null) return WebResponseContent.Instance.Error("未找到此好友");
            if (userInfo.Enable != UserStatus.Enable) return WebResponseContent.Instance.Error("此好友已经注销");

            var entity = _repository.FindAsIQueryable(m => m.FriendUserId == friendUserId && m.CreateID == UserContext.Current.UserId).AsTracking().FirstOrDefault();
            bool result = false;
            if (entity == null)
            {
                entity = new User_Friend()
                {
                    FriendUserId = friendUserId,
                    Status = FriendStatus.ToBeAgreed,
                    FriendRemarkName = nickName ?? userInfo.NickName,
                    Description = resean
                };
                entity.SetCreateDefaultVal();
                _repository.Add(entity);
                result = _repository.SaveChanges() > 0;
            }
            else
            {
                if (entity.Status == FriendStatus.ToBeAgreed)
                    return WebResponseContent.Instance.Error("不要重复添加");
                entity.Status = FriendStatus.ToBeAgreed;
                entity.FriendRemarkName = nickName ?? entity.FriendRemarkName;
                entity.Description = resean;
                entity.ModifyDate = DateTime.Now;
                result = _repository.SaveChanges() > 0;
            }
            if (result)
                Task.Factory.StartNew(() =>
                {
                    ImHelper.SendNotice(UserContext.Current.UserId, friendUserId, "好友请求", ChatType.FriendRequest);
                });
            return WebResponseContent.Instance.Info("请求发送成功", result);
        }

        public int DelFrinend(int friendUserId)
        {
            //_repository.Delete(m => m.CreateID == friendUserId && m.FriendUserId == userId);
            _repository.Delete(m => m.CreateID == UserContext.Current.UserId && m.FriendUserId == friendUserId);
            return _repository.SaveChanges();
        }

        public object GetFriendInfo(int friendUserId)
        {
            int userId = UserContext.Current.UserId;
            var friendInfo = _repository.FindFirst(m => m.FriendUserId == friendUserId && m.CreateID == userId);
            if (friendInfo?.Status == FriendStatus.Black) return WebResponseContent.Instance.Error("该好友已被拉黑");

            bool isFriend = friendInfo?.FriendUserId > 0;
            var friendName = friendInfo?.FriendRemarkName;
            var userInfo = _userRepository.FindAsIQueryable(m => m.User_Id == friendUserId && m.Enable == UserStatus.Enable)
                                            .Select(m => new
                                            {
                                                UserId = m.User_Id,
                                                m.NickName,
                                                m.UserName,
                                                m.Province,
                                                m.City,
                                                m.Avatar,
                                                m.Address,
                                                IsFriend = isFriend,
                                                FriendName = friendName
                                            }).FirstOrDefault();
            if (userInfo == null) return WebResponseContent.Instance.Error("该用户已经注销");
            var currentUserTag = new User_Tag_Member();
            if (userInfo.IsFriend)
                currentUserTag = _tag_MemberRepository.FindFirst(m => m.CreateID == userId && m.UserId == friendUserId);
            return new
            {
                userInfo,
                tagName = currentUserTag?.TagName,
                tagId = currentUserTag?.TagId,
                pics = userInfo.IsFriend ? _fileRepository.FindAsIQueryable(m => m.CreateID == friendUserId && m.IsFriend).Select(m => m.UploadPath).Take(3).ToArray() : null
            };
        }
        public WebResponseContent SendVerification(int receiveUserId)
        {
            int currentUserId = UserContext.Current.UserId;
            var myFriendInfo = _repository.FindFirst(m => m.FriendUserId == receiveUserId && m.CreateID == currentUserId);
            var hisFriendInfo = _repository.FindFirst(m => m.FriendUserId == currentUserId && m.CreateID == receiveUserId);
            var hisUserInfo = _userRepository.FindFirst(m => m.User_Id == receiveUserId && m.Enable == UserStatus.Enable);
            if (hisUserInfo == null) return WebResponseContent.Instance.Error("该好友已经注销.");
            if (myFriendInfo != null)
            {
                //if (myFriendInfo.Status == FriendStatus.ToBeAgreed)
                //    return WebResponseContent.Instance.Error("对方还未同意请求");
                if (myFriendInfo.Status == FriendStatus.Black)
                    return WebResponseContent.Instance.Error("您已经拉黑此好友");
                //if (myFriendInfo.Status == FriendStatus.Reject)
                //    return WebResponseContent.Instance.Error("您已经拒绝此好友");
            }
            else
            {
                _repository.Add(new User_Friend()
                {
                    FriendUserId = receiveUserId,
                    FriendRemarkName = hisUserInfo.NickName,
                    Status = FriendStatus.Temp
                }.SetCreateDefaultVal(), true);
            }
            if (hisFriendInfo != null)
            {
                //if (hisFriendInfo.Status == FriendStatus.ToBeAgreed)
                //    return WebResponseContent.Instance.Error("您还未同意对方请求");
                if (hisFriendInfo.Status == FriendStatus.Black)
                    return WebResponseContent.Instance.Error("好友已经拉黑您");
                //if (hisFriendInfo.Status == FriendStatus.Reject)
                //    return WebResponseContent.Instance.Error("好友拒绝您的请求");
            }
            else
            {
                _repository.Add(new User_Friend()
                {
                    FriendUserId = currentUserId,
                    FriendRemarkName = UserContext.Current.NickName,
                    Status = FriendStatus.Temp,
                    CreateDate = DateTime.Now,
                    CreateID = receiveUserId,
                    Creator = hisUserInfo.UserName
                }, true);
            }
            return WebResponseContent.Instance.OK("验证通过");
        }
        public object GetChatFriendInfo(int userId)
        {
            int currentUserId = UserContext.Current.UserId;
            var myFriendInfo = _repository.FindFirst(m => m.FriendUserId == userId && m.CreateID == currentUserId);
            var userInfo = _userRepository.FindFirst(m => m.User_Id == userId && m.Enable == UserStatus.Enable);
            return new
            {
                UserId = userId,
                userInfo.Avatar,
                NickName = myFriendInfo?.FriendRemarkName ?? userInfo.NickName,
                myFriendInfo?.Status,
                myFriendInfo?.BackgroundImage,
                userInfo.Remark
            };
        }
        public IOrderedEnumerable<KeyValuePair<char, IList<User_Friend>>> GetMyFriends()
        {
            int userId = UserContext.Current.UserId;
            if (!_repository.Exists(m => m.FriendUserId == userId && m.CreateID == userId))
                _repository.Add(new User_Friend() { FriendUserId = userId, Status = FriendStatus.Friend, FriendRemarkName = UserContext.Current.NickName, }.SetCreateDefaultVal(), true);
            var list = _repository.FindAsIQueryable(m => m.CreateID == userId && m.Status == FriendStatus.Friend).Take(1000)
                      .Join(repository.DbContext.Set<Sys_User>(), f => f.FriendUserId, u => u.User_Id, (f, u) => new User_Friend().Init(f, u.Avatar)).ToList();
            Dictionary<char, IList<User_Friend>> keyValuePairs = new();
            list.ForEach(m =>
            {
                var label = m.FriendRemarkName.GetSpell();
                if (keyValuePairs.ContainsKey(label))
                    keyValuePairs[label].Add(m);
                else
                    keyValuePairs.Add(label, new List<User_Friend>() { m });
            });
            return keyValuePairs.OrderBy(m => m.Key);
        }



        public IQueryable<User_Friend> GetToBeAgreeFriends()
        {
            int userId = UserContext.Current.UserId;
            return _repository.FindAsIQueryable(m => m.FriendUserId == userId && m.Status == FriendStatus.ToBeAgreed).Take(5)
                              .Join(_userRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable),
                              m => m.CreateID,
                              n => n.User_Id,
                              (m, n) => new User_Friend().Init(m, n.Avatar, n.NickName));

        }

        public IQueryable<User_Friend> GetBlackListFriends()
        {
            int userId = UserContext.Current.UserId;
            return _repository.FindAsIQueryable(m => m.CreateID == userId && m.Status == FriendStatus.Black).Take(100)
                              .Join(_userRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable),
                              m => m.CreateID,
                              n => n.User_Id,
                              (m, n) => new User_Friend().Init(m, n.Avatar, n.NickName)); 
        }
        public IQueryable<User_Friend> GetAllFriendsToMe()
        {
            int userId = UserContext.Current.UserId;
            return _repository.FindAsIQueryable(m => m.FriendUserId == userId && m.Status != FriendStatus.Temp).OrderByDescending(m => m.CreateDate).Take(1000)
                              .Join(_userRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable),
                              m => m.FriendUserId,
                              n => n.User_Id,
                              (m, n) => new User_Friend().Init(m, n.Avatar));

        }
        public IList<User_Friend> GetAllFriendsToOther()
        {
            int userId = UserContext.Current.UserId;
            var list = _repository.FindAsIQueryable(m => m.CreateID == userId).OrderByDescending(m => m.CreateDate).Take(1000)
                              .Join(_userRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable),
                              m => m.FriendUserId,
                              n => n.User_Id,
                              (m, n) => new User_Friend().Init(m, n.Avatar)).ToList();
            list.ForEach(m => { m.PinYin = m.FriendRemarkName?.GetChineseSpell(); });
            return list;
        }
        public object SearchAddUser(string key)
        {
            return _userRepository.FindAsIQueryable(m => m.PhoneNo == key || m.UserName == key)
                .Select(m => new { FriendUserId = m.User_Id, FriendRemarkName = m.NickName, m.Avatar }).FirstOrDefault();

        }

        public User_Friend GetFriendDetail(int friendUserId)
        {
            var friend = _repository.FindFirst(m => m.FriendUserId == friendUserId && m.CreateID == UserContext.Current.UserId);
            var userInfo = _userRepository.FindFirst(m => m.User_Id == friendUserId);
            if (friend == null)
                friend = new User_Friend() { FriendRemarkName = userInfo.NickName, FriendUserId = userInfo.User_Id };
            friend.Avatar = userInfo.Avatar;
            friend.FriendUserName = userInfo.NickName;
            return friend;
        }
        public User_Friend FriendSetting(int friendUserId, bool? notDisturb, bool? top, bool? black, string remarkName, string backGroundImg)
        {

            var entity = _repository.FindAsIQueryable(m => m.FriendUserId == friendUserId && m.CreateID == UserContext.Current.UserId).FirstOrDefault();
            if (entity == null)
            {
                var userInfo = _userRepository.FindFirst(m => m.User_Id == friendUserId);
                _repository.Add(new User_Friend()
                {
                    FriendUserId = friendUserId,
                    Status = FriendStatus.Temp,
                    FriendRemarkName = userInfo?.NickName
                }.SetCreateDefaultVal(), true);
                entity = _repository.FindAsIQueryable(m => m.FriendUserId == friendUserId && m.CreateID == UserContext.Current.UserId).AsTracking().FirstOrDefault();
            }
            if (notDisturb != null)
                entity.NotDisturb = (bool)notDisturb;
            if (top != null)
                entity.Top = (bool)top;
            if (black != null)
                entity.Status = black.ToBool() ? FriendStatus.Black : FriendStatus.Friend;
            if (backGroundImg.IsNotNullOrEmpty())
                entity.BackgroundImage = backGroundImg;
            if (remarkName.IsNotNullOrEmpty())
                entity.FriendRemarkName = remarkName;
            entity.ModifyDate = DateTime.Now;
            _repository.Update(entity, true);
            return entity;
        }

        public IQueryable<object> GetNewestUsers(DateTime registerDate)
        {
            return _userRepository.FindAsIQueryable(m => m.CreateDate < registerDate).Select(m => new { userId = m.User_Id, m.NickName, m.Avatar, m.CreateDate })
                .OrderByDescending(m => m.CreateDate).Take(10);
        }
    }
}
