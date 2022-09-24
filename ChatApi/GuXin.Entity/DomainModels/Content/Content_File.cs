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
    [Entity(TableCnName = "内容文件", TableName = "Content_File")]
    public class Content_File : BaseEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "Guid")]
        [Column(TypeName = "uniqueidentifier")]
        [Required(AllowEmptyStrings = false)]
        public Guid Guid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ContentGuid")]
        [Column(TypeName = "uniqueidentifier")]
        [Required(AllowEmptyStrings = false)]
        public Guid ContentGuid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Name")]
        [MaxLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "UploadPath")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        [Required(AllowEmptyStrings = false)]
        public string UploadPath { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ContentType")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string ContentType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Length")]
        [Column(TypeName = "float")]
        [Required(AllowEmptyStrings = false)]
        public float Length { get; set; }

        [Display(Name = "Height")]
        [Column(TypeName = "float")]
        public float Height { get; set; }

        [Display(Name = "Width")]
        [Column(TypeName = "float")]
        public float Width { get; set; }

        [Display(Name = "Orientation")]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Orientation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "IsFriend")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool IsFriend { get; set; }

        [Display(Name = "IsPublic")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool IsPublic { get; set; }

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


    }
}