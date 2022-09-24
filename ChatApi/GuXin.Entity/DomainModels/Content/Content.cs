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
    [Entity(TableCnName = "内容", TableName = "Content", DetailTable = new Type[] { typeof(Content_File) }, DetailTableCnName = "内容文件")]
    public class Content : BaseEntity
    {
        /// <summary>
        ///转发ID
        /// </summary>
        [Display(Name = "转发ID")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? ParentGuid { get; set; }

        /// <summary>
        ///内容
        /// </summary>
        [Display(Name = "内容")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Required(AllowEmptyStrings = false)]
        public string Contents { get; set; }

        /// <summary>
        ///内容审核状态
        /// </summary>
        [Display(Name = "内容审核状态")]
        [Column(TypeName = "smallint")]
        [Required(AllowEmptyStrings = false)]
        public int AuditStatus { get; set; }

        /// <summary>
        ///图片个数
        /// </summary>
        [Display(Name = "图片个数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int PicCount { get; set; }

        /// <summary>
        ///收藏数
        /// </summary>
        [Display(Name = "收藏数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int CollectCount { get; set; }

        /// <summary>
        ///分享转发数
        /// </summary>
        [Display(Name = "分享转发数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int ShareCount { get; set; }

        /// <summary>
        ///点赞数
        /// </summary>
        [Display(Name = "点赞数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int PraiseCount { get; set; }

        /// <summary>
        ///新赞数
        /// </summary>
        [Display(Name = "新赞数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int XinPraiseCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "CommentCount")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int CommentCount { get; set; }

        [Display(Name = "朋友圈内容")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool IsFriend { get; set; }

        [Display(Name = "公共内容")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool IsPublic { get; set; }

        /// <summary>
        ///可见天数
        /// </summary>
        [Display(Name = "可见天数")]
        [Column(TypeName = "smallint")]
        public int? VisibleDays { get; set; }

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
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string Auditor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "ContentGuid")]
        [Column(TypeName = "uniqueidentifier")]
        [Required(AllowEmptyStrings = false)]
        public Guid ContentGuid { get; set; }

        [Display(Name = "图片文件")]
        [ForeignKey("ContentGuid")]
        public List<Content_File> ContentFiles { get; set; }


        [Display(Name = "用户信息")]
        [ForeignKey("CreateID")]
        public Sys_User UserInfo { get; set; }


        [Display(Name = "评论用户")]
        [ForeignKey("ContentGuid")]
        public List<Content_Comment> Comments { get; set; }


        [Display(Name = "点赞用户")]
        [ForeignKey("ContentGuid")]
        public List<Content_Praise> Praises { get; set; }

    }
}