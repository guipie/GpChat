using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppEnum
{
    /// <summary>
    /// 获取新赞类别
    /// </summary>
    public enum XinPraiseType
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login = 1,
        /// <summary>
        /// 邀请注册
        /// </summary>
        Invite = 2

    }
    public enum ComplaintType
    {

        /// <summary>
        /// 简介投诉
        /// </summary>
        intro = 1,
        /// <summary>
        /// 内容投诉
        /// </summary>
        content = 2,
        /// <summary>
        /// 用户投诉
        /// </summary>
        user = 3,
        /// <summary>
        /// 聊天投诉
        /// </summary>
        chat = 4
    }

}
