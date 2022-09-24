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
    [Entity(TableCnName = "系统公告", TableName = "User_Sys_Notice")]
    public class User_Sys_Notice : BaseEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "Id")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Type")]
        [Column(TypeName = "nvarchar(20)"), Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Title")]
        [MaxLength(40), Editable(true)]
        [Column(TypeName = "nvarchar(40)")]
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Display(Name = "Summary")]
        [MaxLength(200), Editable(true)]
        [Column(TypeName = "nvarchar(200)")] 
        public string Summary { get; set; } 

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Contents")]
        [Column(TypeName = "nvarchar(max)"), Editable(true)] 
        public string Contents { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateDate")]
        [Column(TypeName = "datetime")]
        [Required(AllowEmptyStrings = false), Editable(true)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false), Editable(true)]
        public int CreateID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Creator")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)"), Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Creator { get; set; }


    }
}