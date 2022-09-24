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
using GuXin.Entity.AppEnum;
using GuXin.Entity.SystemModels;

namespace GuXin.Entity.DomainModels
{
    [Entity(TableCnName = "新赞记录", TableName = "User_XinPraise_Record")]
    public class User_XinPraise_Record : BaseEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "Id")]
        [Column(TypeName = "bigint")]
        [Required(AllowEmptyStrings = false)]
        public long Id { get; set; }

        /// <summary>
        ///
        /// </summary>
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
        [Display(Name = "PraiseType")]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]
        [Required(AllowEmptyStrings = false)]
        public XinPraiseType PraiseType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateDate")]
        [Column(TypeName = "datetime")]
        [Required(AllowEmptyStrings = false)]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int? CreateID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Creator")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string Creator { get; set; }


    }
}