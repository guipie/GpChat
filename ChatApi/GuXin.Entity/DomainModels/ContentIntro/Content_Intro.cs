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
    [Entity(TableCnName = "简介", TableName = "Content_Intro")]
    public class Content_Intro : BaseEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "IntroGuid")]
        [Column(TypeName = "uniqueidentifier")]
        [Required(AllowEmptyStrings = false)]
        public Guid IntroGuid { get; set; }

        /// <summary>
        ///开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Column(TypeName = "varchar(7)")]
        public string BeginDate { get; set; }

        /// <summary>
        ///结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        [Column(TypeName = "varchar(7)")]
        public string EndDate { get; set; }

        /// <summary>
        ///位置
        /// </summary>
        [Display(Name = "位置")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        public string Location { get; set; }

        /// <summary>
        ///经历
        /// </summary>
        [Display(Name = "经历")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string Introduction { get; set; }

        /// <summary>
        ///图片个数
        /// </summary>
        [Display(Name = "图片个数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int PicCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CreateDate")]
        [Column(TypeName = "datetime")]
        [Required(AllowEmptyStrings = false)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "ModifyDate")]
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }

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

        /// <summary>
        ///新赞数
        /// </summary>
        [Display(Name = "新赞数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int PraiseCount { get; set; }



        [Display(Name = "图片文件")]
        [ForeignKey("ContentGuid")]
        public List<Content_File> ContentFiles { get; set; }

    }
}