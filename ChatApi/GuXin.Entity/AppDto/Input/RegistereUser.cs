using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Input
{
    public class AppRegistereUser
    {
        [Display(Name = "手机号")]
        [StringLength(11), Required(AllowEmptyStrings = false)]
        public string PhoneNo { get; set; }
        [Display(Name = "注册验证码")]
        [StringLength(4), Required(AllowEmptyStrings = false)]
        public int Vcode { get; set; }
        [Display(Name = "邀请码")]
        [MaxLength(6)]
        public string InCode { get; set; }
    }
}
