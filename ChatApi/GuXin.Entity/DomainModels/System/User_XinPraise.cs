using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.DomainModels.System
{
    public class User_XinPraise
    {

        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "UserId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int UserId { get; set; }

        /// <summary>
        /// 新赞数量
        /// </summary>
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int Quantity { get; set; }
        /// <summary>
        /// 可使用新赞
        /// </summary>
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int UsedCount { get; set; }
        /// <summary>
        /// 下次可使用新赞
        /// </summary>
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int NextUsedCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ModifyID")]
        [Column(TypeName = "int")]
        public int? ModifyID { get; set; }

        /// <summary>
        ///修改人
        /// </summary>
        [Display(Name = "修改人")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string Modifier { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }
    }
}
