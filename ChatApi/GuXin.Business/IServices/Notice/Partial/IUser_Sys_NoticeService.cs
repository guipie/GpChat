/*
*所有关于User_Sys_Notice类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace GuXin.Business.IServices
{
    public partial interface IUser_Sys_NoticeService
    {
        /// <summary>
        /// 获取当前用户系统通知配置
        /// </summary>
        /// <returns></returns>
        User_Sys_Notice_Setting Get();
        /// <summary>
        /// 清理当前系统通知
        /// </summary>
        /// <param name="settingId"></param>
        void NoticeClear(int settingId);
        /// <summary>
        /// 获取系统通知
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        IQueryable<object> Notices(DateTime searchDate);
        /// <summary>
        /// 系统通知相关设置
        /// </summary>
        /// <param name="isTop"></param>
        /// <param name="isNotDisturb"></param>
        /// <returns></returns>
        bool NoticeSetting(bool? isTop, bool? isNotDisturb);
    }
}
