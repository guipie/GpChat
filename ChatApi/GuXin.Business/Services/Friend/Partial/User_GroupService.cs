/*
 *所有关于User_Group_Setting类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*User_Group_SettingService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using GuXin.Business.Repositories;
using GuXin.Entity.AppDto.Output;
using GuXin.Core.ManageUser;
using GuXin.Core.Enums;
using GuXin.Entity.AppEnum;
using GuXin.Entity.AppDto.Common;
using System;
using System.Collections.Generic;

namespace GuXin.Business.Services
{
    public partial class User_GroupService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser_Group_SettingRepository _repository;//访问数据库
        private readonly IUser_GroupRepository _GroupRepository;//访问数据库
        private readonly IUser_Group_MemberRepository _Group_MemberRepository;//访问数据库
        private readonly IUserRepository _userRepository;//访问数据库


        [ActivatorUtilitiesConstructor]
        public User_GroupService(
            IUser_Group_SettingRepository dbRepository,
            IUser_GroupRepository user_GroupRepository,
            IUser_Group_MemberRepository user_Group_MemberRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _GroupRepository = user_GroupRepository;
            _Group_MemberRepository = user_Group_MemberRepository;
            _userRepository = userRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }



        public GroupInfo GetGroupInfo(int groupId)
        {
            var group = _GroupRepository.FindFirst(m => m.GroupId == groupId);
            if (group == null) return new GroupInfo()
            {
                Name = "群已经解散",
                Members = new List<GroupMember>() {
                new GroupMember() { Avatar=UserContext.Current.UserInfo.Avatar,NickName=UserContext.Current.NickName,UserId=UserContext.Current.UserId } }.AsQueryable()
            };
            User_Group_Setting group_Setting = _repository.FindAsIQueryable(m => m.GroupId == groupId).FirstOrDefault();
            GroupInfo groupInfo = new(group_Setting ?? new User_Group_Setting(), group.GroupId, group.Name, group.Description, group.CreateID, group.CreateDate, group.ModifyDate);

            groupInfo.Members = from members in _Group_MemberRepository.FindAsIQueryable(m => m.GroupId == groupInfo.GroupId)
                                join users in _userRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable)
                                on members.UserId equals users.User_Id
                                select new GroupMember { NickName = members.NickName, UserId = members.UserId, Avatar = users.Avatar };
            return groupInfo;
        }

        public int[] GetAllGroups()
        {
            var groupIds = _Group_MemberRepository.FindAsIQueryable(m => m.UserId == UserContext.Current.UserId).Select(m => m.GroupId).Distinct().ToArray();
            return groupIds;
        }
        public IList<GroupInfo> GetAllGroupsInfo()
        {
            var groupIds = GetAllGroups();
            var list = (from g in _GroupRepository.FindAsIQueryable(m => groupIds.Contains(m.GroupId))
                        join s in _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId).OrderBy(m => m.CreateDate).Take(9) on g.GroupId equals s.GroupId into temp
                        from tt in temp.DefaultIfEmpty()
                        select new GroupInfo(tt ?? new User_Group_Setting(), g.GroupId, g.Name, g.Description, g.CreateID, g.CreateDate, g.ModifyDate)).ToList();
            list.ForEach(x =>
            {
                x.Members = from members in _Group_MemberRepository.FindAsIQueryable(m => m.GroupId == x.GroupId)
                            join users in _userRepository.FindAsIQueryable(m => m.Enable == UserStatus.Enable)
                            on members.UserId equals users.User_Id
                            select new GroupMember
                            {
                                NickName = members.NickName,
                                UserId = members.UserId,
                                Avatar = users.Avatar,
                                CreateDate = members.CreateDate
                            };
            });
            return list;
        }
        public IQueryable<object> GetMySaveGroups()
        {
            return (from g in _GroupRepository.FindAsIQueryable(m => true)
                    join s in _repository.FindAsIQueryable(m => m.CreateID == UserContext.Current.UserId && m.SaveGroup).OrderBy(m => m.CreateDate).Take(9)
                    on g.GroupId equals s.GroupId into temp
                    from tt in temp.DefaultIfEmpty()
                    select new
                    {
                        g.GroupId,
                        g.Name,
                        Id = tt == null ? 0 : tt.Id,
                        NotDisturb = tt != null && tt.NotDisturb,
                        Top = tt != null && tt.Top,
                        SaveGroup = tt != null && tt.SaveGroup,
                        AcceptAdd = tt != null && tt.AcceptAdd,
                        CreateDate = tt == null ? DateTime.Now : tt.CreateDate,
                        ModifyDate = tt == null ? DateTime.Now : tt.ModifyDate
                    });
        }
        public bool GroupSettings(int groupId, bool? notDisturb, bool? top, bool? saveMy, bool? accept)
        {
            var mySettings = _repository.FindAsIQueryable(m => m.GroupId == groupId && m.CreateID == UserContext.Current.UserId).AsTracking().FirstOrDefault();
            if (mySettings == null)
                mySettings = new User_Group_Setting();
            if (notDisturb != null)
                mySettings.NotDisturb = notDisturb.Value;
            if (top != null)
                mySettings.Top = top.Value;
            if (saveMy != null)
                mySettings.SaveGroup = saveMy.Value;
            if (accept != null)
                mySettings.AcceptAdd = accept.Value;
            if (mySettings.Id > 0)
                _repository.SaveChanges();
            else
            {
                mySettings.GroupId = groupId;
                _repository.Add(mySettings.SetCreateDefaultVal(), true);
            }
            return mySettings.Id > 0;
        }
        public bool GroupNameDiscription(int groupId, string name, string discription)
        {
            var group = _GroupRepository.FindAsIQueryable(m => m.GroupId == groupId).AsTracking().FirstOrDefault();
            if (name.IsNotNullOrEmpty())
                group.Name = name;
            if (discription.IsNotNullOrEmpty())
                group.Description = discription;
            return _repository.SaveChanges() > 0;
        }
        public WebResponseContent SetNickName(int groupId, string nickName)
        {
            var myMember = _Group_MemberRepository.FindAsIQueryable(m => m.GroupId == groupId && m.UserId == UserContext.Current.UserId).AsTracking().FirstOrDefault();
            if (myMember == null) return WebResponseContent.Instance.Error(ResponseType.GroupMemberRemove, "您已经被移除群聊");
            myMember.NickName = nickName;
            return WebResponseContent.Instance.Info(_repository.SaveChanges() > 0);
        }
        public WebResponseContent Post(int[] userIds)
        {

            return _repository.DbContextBeginTransaction(() =>
                   {
                       var group = new User_Group() { Name = "群聊" }.SetCreateDefaultVal();
                       _GroupRepository.Add(group, true);
                       var members = _userRepository.FindAsIQueryable(m => userIds.Contains(m.User_Id) && m.Enable == UserStatus.Enable).Select(m => new { m.User_Id, m.NickName }).ToList()
                                                    .Select(m => new User_Group_Member() { GroupId = group.GroupId, UserId = m.User_Id, NickName = m.NickName }.SetCreateDefaultVal());
                       _Group_MemberRepository.AddRange(members);
                       if (!userIds.Contains(UserContext.Current.UserId))
                           _Group_MemberRepository.Add(new User_Group_Member() { GroupId = group.GroupId, UserId = UserContext.Current.UserId, NickName = UserContext.Current.NickName }.SetCreateDefaultVal());
                       _repository.Add(new User_Group_Setting()
                       {
                           AcceptAdd = true,
                           GroupId = group.GroupId,
                           SaveGroup = true,
                           Top = false
                       }.SetCreateDefaultVal());
                       return WebResponseContent.Instance.Info(_repository.SaveChanges() > 0, group.GroupId);
                   });
        }

        public WebResponseNormal Put(int groupId, int[] userIds)
        {
            var addedMembers = _userRepository.FindAsIQueryable(m => userIds.Contains(m.User_Id)).Select(m => new { UserId = m.User_Id, m.NickName, m.Avatar }).ToList();
            addedMembers.ForEach(m =>
            {
                _Group_MemberRepository.Add(new User_Group_Member() { GroupId = groupId, UserId = m.UserId, NickName = m.NickName }.SetCreateDefaultVal(), true);
                ImHelper.JoinChan(m.UserId, groupId.ToString());
            });
            return WebResponseNormal.Instance.OK(addedMembers);
        }
        public WebResponseContent Delete(int groupId, int[] userIds)
        {
            if (!_GroupRepository.Exists(m => m.GroupId == groupId && m.CreateID == UserContext.Current.UserId))
                return WebResponseContent.Instance.Error("您没有权限移除别人");
            if (userIds.Contains(UserContext.Current.UserId)) return WebResponseContent.Instance.Error("自己不能移除自己");
            _Group_MemberRepository.Delete(m => userIds.Contains(m.UserId) && m.GroupId == groupId, true);
            foreach (int userId in userIds)
                ImHelper.LeaveChan(userId, groupId.ToString());
            return WebResponseContent.Instance.OK();
        }
        public WebResponseContent Quit(int groupId)
        {
            _Group_MemberRepository.Delete(m => m.UserId == UserContext.Current.UserId && m.GroupId == groupId, true);
            repository.Delete(m => m.CreateID == UserContext.Current.UserId && m.GroupId == groupId, true);
            ImHelper.LeaveChan(UserContext.Current.UserId, groupId.ToString());
            return WebResponseContent.Instance.OK();
        }
    }
}
