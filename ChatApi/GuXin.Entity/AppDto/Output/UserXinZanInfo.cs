using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    public class UserXinZanInfo
    {
        public int quantity { get; set; }
        public int todayUsed { get; set; }
        public int nextUsed { get; set; }

        /// <summary>
        /// 邀请用户
        /// </summary>
        public IQueryable<object> invitedUsers { get; set; }

        public string invitedCode { get; set; }
    }
}
