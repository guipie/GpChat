using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Input
{
    public class AppLogin
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [StringLength(11), Required(AllowEmptyStrings = false)]
        public string PhoneNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [MinLength(6)]
        public string PassWord { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码")]
        [MinLength(6)]
        public string ConfirmPassWord { get; set; }
        /// <summary>
        ///  验证码
        /// </summary>
        [Display(Name = " 验证码")]
        [StringLength(4)]
        public int? Vcode { get; set; }
    }
}
