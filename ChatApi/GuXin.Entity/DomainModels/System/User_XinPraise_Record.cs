using GuXin.Entity.AppEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.DomainModels.System
{
    public class User_XinPraise_Record
    {

        [Key]
        [Display(Name = "Id")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "UserId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int UserId { get; set; }

        /// <summary>
        /// 获取数量，默认一个
        /// </summary>
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int Quantity { get; set; }
        /// <summary>
        /// 可使用新赞
        /// </summary>
        [Column(TypeName = "varchar(10)")]
        [Required(AllowEmptyStrings = false)]
        public XinPraiseType PraiseType { get; set; }


        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int CreateID { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        [Display(Name = "创建人")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(20)")]
        public string Creator { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
    }
}
