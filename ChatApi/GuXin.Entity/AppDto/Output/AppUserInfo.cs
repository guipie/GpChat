using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    public class AppUserInfo
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string NickName { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }
        public int? Sex { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Remark { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
