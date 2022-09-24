using System;
using System.Collections.Generic;
using System.Text;

namespace GuXin.ImCore.Models
{
    public class NoticeObject
    {
        /// <summary>
        /// 通知数量
        /// </summary>
        public int Count { get; set; }
        public string Content { get; set; }
        public DateTime SendTime { get; set; }
    }
}
