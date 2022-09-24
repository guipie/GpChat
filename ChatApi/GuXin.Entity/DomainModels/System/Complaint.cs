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
    [Entity(TableCnName = "投诉内容", TableName = "Complaint")]
    public class Complaint : BaseEntity
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
        ///投诉类型
        /// </summary>
        [Display(Name = "投诉类型")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string ComplaintDic { get; set; }

        /// <summary>
        ///投诉内容类别
        /// </summary>
        [Display(Name = "投诉内容类别")]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string ComplaintType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ComplaintGuid")]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string ComplaintGuid { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(4000)]
        [Column(TypeName = "nvarchar(4000)")]
        public string Remark { get; set; }

        /// <summary>
        ///图片
        /// </summary>
        [Display(Name = "图片")]
        [MaxLength(4000)]
        [Column(TypeName = "nvarchar(4000)")]
        public string Pics { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateDate")]
        [Column(TypeName = "datetime")]
        [Required(AllowEmptyStrings = false)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int CreateID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Creator")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string Creator { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "AuditDate")]
        [Column(TypeName = "datetime")]
        public DateTime? AuditDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Auditor")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        public string Auditor { get; set; }

        /// <summary>
        ///内容审核状态
        /// </summary>
        [Display(Name = "内容审核状态")]
        [Column(TypeName = "smallint")]
        [Required(AllowEmptyStrings = false)]
        public int AuditStatus { get; set; }


    }
}