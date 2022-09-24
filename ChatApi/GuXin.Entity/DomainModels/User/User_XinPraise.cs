/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuXin.Entity.SystemModels;

namespace GuXin.Entity.DomainModels
{
    [Entity(TableCnName = "用户新赞汇总", TableName = "User_XinPraise")]
    public class User_XinPraise : BaseEntity
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
        ///
        /// </summary>
        [Display(Name = "Quantity")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int Quantity { get; set; }



        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Modifier")]
        [MaxLength(60)]
        [Column(TypeName = "nvarchar(20)")]
        public string Modifier { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ModifyDate")]
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ModifyID")]
        [Column(TypeName = "int")]
        public int? ModifyID { get; set; }


    }
}