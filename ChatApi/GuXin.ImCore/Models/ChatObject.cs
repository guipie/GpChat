using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuXin.ImCore
{
    public class ChatObject
    {
        public int SendUserId { get; set; }
        public int ReceiveUserId { get; set; }
        public DateTime? SendTime { get; set; }
        public string Content { get; set; }
        public ChatType Type { get; set; }
        public int IsGroup { get; set; }
        public JObject FileInfo { get; set; }

    }
    public enum ChatType
    {
        /// <summary>
        /// 故新消息
        /// </summary>
        ChatText = 1,
        ChatImage = 2,
        ChatVoice = 3,
        ChatContent = 4,

        #region 好友操作
        FriendRequest = 21,
        FriendAccept = 22,
        FriendDel = 23,
        #endregion
        #region 内容操作
        Comment = 24,
        Praise = 25,
        XinPraise = 26,
        #endregion
        #region 关注相关
        Follow = 27,
        #endregion
        #region  系统相关
        Notice = 100
        #endregion
    }
    ///// <summary>
    ///// 消息通知类别 0消息，1系统通知，2分享好友，3分享内容
    ///// </summary>
    //public enum GxNoticeType
    //{
    //    Chat = 0,
    //    Sys = 1,
    //    ShareFriend = 2,
    //    ShareContent = 3
    //}
}
