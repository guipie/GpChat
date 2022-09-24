using GuXin.Entity.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuXin.Entity.AppEnum;

namespace GuXin.Entity.DomainModels
{
    public class UserInfo
    {
        public int User_Id { get; set; }
        /// <summary>
        /// 多个角色ID
        /// </summary>
        public int Role_Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public UserStatus Enable { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }

        public IList<int> XinZan { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
