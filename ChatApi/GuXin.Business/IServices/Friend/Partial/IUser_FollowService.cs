/*
*所有关于User_Follow类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace GuXin.Business.IServices
{
    public partial interface IUser_FollowService
    {
        /// <summary>
        /// 获取关注通知设置
        /// </summary>
        /// <returns></returns>
        User_Follow_Setting GetCurrentSettings();
        /// <summary>
        /// 是否置顶互动通知
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        int IsTop(bool top);
        int IsNotDisturb(bool notDisturb);
        /// <summary>
        /// 是否禁用互动通知
        /// </summary>
        /// <param name="disable"></param>
        /// <returns></returns>
        int IsDisable(bool disable);

        /// <summary>
        /// 清理所有互动通知
        /// </summary>
        /// <returns></returns>
        int ClearInteractNotices();
        WebResponseContent FollowOrUnFollow(int userId);
        IQueryable<object> GetFollowUsers(DateTime date);
    }
}
