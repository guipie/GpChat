using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppEnum
{
    /// <summary>
    /// 手机验证码类别
    /// </summary>
    public enum VCode
    {
        /// <summary>
        /// 登录验证码
        /// </summary>
        Lcode = 1,
        /// <summary>
        /// 注册验证码
        /// </summary>
        Rcode = 2,
        /// <summary>
        /// 修改密码
        /// </summary>
        Ucode = 3,
        /// <summary>
        /// 更改手机号
        /// </summary>
        Ccode = 4
    }
}
