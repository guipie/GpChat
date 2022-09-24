using System;
using System.Collections.Generic;
using System.Text;

namespace GuXin.ImCore.Models
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public class RedisKey
    {
        /// <summary>
        /// 未读聊天消息
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string ChatKey(string prefix, Guid userId) => $"{prefix}${userId}UnreadMsg";
        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="userId"></param> 
        /// <param name="type">通知类型</param> 
        /// <returns></returns>
        public static string NoticeKey(string prefix, Guid userId, ChatType type)
        {
            switch (type)
            {
                case ChatType.Comment:
                case ChatType.Praise:
                case ChatType.XinPraise:
                    return $"{prefix}${userId}InteractNotice";
                default:
                    return $"{prefix}${userId}{type}Notice";
            }
        }
    }
}
