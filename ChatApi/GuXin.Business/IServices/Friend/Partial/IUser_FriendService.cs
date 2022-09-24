/*
*所有关于User_Friend类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;
using System;
using GuXin.Entity.AppEnum;
using System.Collections.Generic;

namespace GuXin.Business.IServices
{
    public partial interface IUser_FriendService
    {

        object SearchAddUser(string key);
        WebResponseContent AddFriend(int friendUserId, string nickName, string resean);
        WebResponseContent AcceptFriend(int friendUserId);
        User_Friend FriendSetting(int friendUserId, bool? notDisturb, bool? top, bool? black, string remarkName, string backGroundImg);
        int DelFrinend(int friendUserId);
        /// <summary>
        /// 好友信息【包含朋友去，tag标签】
        /// </summary>
        /// <param name="friendUserId"></param>
        /// <returns></returns>
        object GetFriendInfo(int friendUserId);
        User_Friend GetFriendDetail(int friendUserId);
        WebResponseContent SendVerification(int receiveUserId);
        object GetChatFriendInfo(int userId);
        IOrderedEnumerable<KeyValuePair<char, IList<User_Friend>>> GetMyFriends();
        //IQueryable<User_Friend> GetMyFriends(FriendStatus friendStatus, string keyword, DateTime addDate);
        IQueryable<User_Friend> GetToBeAgreeFriends();
        IQueryable<User_Friend> GetBlackListFriends();
        IQueryable<object> GetNewestUsers(DateTime registerDate);

        /// <summary>
        /// 获取添加我的所有好友
        /// </summary>
        /// <returns></returns>
        IQueryable<User_Friend> GetAllFriendsToMe();
        /// <summary>
        /// 获取我要联系的所有好友的,包括临时好友
        /// </summary>
        /// <returns></returns>
        IList<User_Friend> GetAllFriendsToOther();

    }
}
