using GuXin.Core.ManageUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Core.CacheManager
{
    public class CacheKeys
    {
        public static int userId => UserContext.Current.UserId;
        /// <summary>
        /// 当前使用信息索引
        /// </summary>
        public static string XinZanIndex = "Xin_Zan" + userId + DateTime.Now.DayOfYear;
    }
}
