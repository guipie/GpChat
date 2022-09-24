using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    /// <summary>
    /// 朋友圈内容
    /// </summary>
    public class FriendContent
    {
        public Guid ContentGuid { get; set; }
        public Guid? ParentGuid { get; set; }
        public object Parent { get; set; }
        public string Contents { get; set; }
        public string[] Pics { get; set; }
        public string Avatar { get; set; }
        public string NickName { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public int PraisesCount { get; set; }
        public int CommentCount { get; set; }

        public IQueryable<object> Comments { get; set; }
        public IQueryable<object> PraiseUsers { get; set; }
    }
}
