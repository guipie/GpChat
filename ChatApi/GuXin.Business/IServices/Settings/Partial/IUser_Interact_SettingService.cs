/*
*所有关于User_Interact_Setting类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;
using System;
using GuXin.Entity.AppDto.Output;
using System.Collections.Generic;

namespace GuXin.Business.IServices
{
    /// <summary>
    /// 互动通知相关
    /// </summary>
    public partial interface IUser_Interact_SettingService
    {
        /// <summary>
        /// 获取当前用户的互动通知设置
        /// </summary>
        /// <returns></returns>
        User_Interact_Setting GetCurrentSettings();
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
        /// <summary>
        /// 获取互动 
        /// </summary>
        /// <returns></returns>
        IList<InteractNoticeDto> GetInteractNotices(DateTime searchDate);
    }
}
