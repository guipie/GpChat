using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppEnum
{
    /// <summary>
    /// 好友状态
    /// </summary>
    public enum FriendStatus
    {
        /// <summary>
        /// 临时好友
        /// </summary>
        Temp = -1,
        /// <summary>
        /// 待同意
        /// </summary>
        ToBeAgreed = 0,
        /// <summary>
        /// 好友
        /// </summary>
        Friend = 1,
        /// <summary>
        /// 拒绝
        /// </summary>
        Reject = 2,
        /// <summary>
        /// 拉黑
        /// </summary>
        Black = 3
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 0,
        /// <summary>
        /// 启用
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 注销
        /// </summary>
        Logout = 2
    }

}
