using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    public class SearchUserContent
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public bool IsFollowing { get; set; }
        public DateTime LastLoginDate { get; set; }
        public IQueryable<object> ContentList { get; set; }
        public SearchUserContent(int userId, string nickName, string avatar,DateTime date)
        {
            UserId = userId;
            NickName = nickName;
            Avatar = avatar;
            LastLoginDate = date;
        }
    }
}
