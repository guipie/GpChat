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
    [Entity(TableCnName = "内容评论", TableName = "Content_Comment")]
    public class Content_Comment : BaseEntity
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
        [Display(Name = "ContentGuid")]
        [Column(TypeName = "uniqueidentifier")]
        [Required(AllowEmptyStrings = false)]
        public Guid ContentGuid { get; set; }

        /// <summary>
        ///评论内容
        /// </summary>
        [Display(Name = "评论内容")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        [Required(AllowEmptyStrings = false)]
        public string Contents { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "PraiseCount")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int PraiseCount { get; set; }

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

        [Display(Name = "朋友圈内容")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool IsFriend { get; set; }

        [Display(Name = "公共内容")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool IsPublic { get; set; }
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
        [Display(Name = "ParentId")]
        [Column(TypeName = "int")]
        public int? ParentId { get; set; }


        [Display(Name = "用户信息")]
        [ForeignKey("CreateID")]
        public Sys_User UserInfo { get; set; }

    }
}