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
    [Entity(TableCnName = "版本升级", TableName = "App_Upgrade")]
    public class App_Upgrade : BaseEntity
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
        ///版本号
        /// </summary>
        [Display(Name = "版本号")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)"), Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Version { get; set; }

        /// <summary>
        ///下载地址
        /// </summary>
        [Display(Name = "下载地址")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)"), Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Url { get; set; }

        /// <summary>
        ///升级描述
        /// </summary>
        [Display(Name = "升级描述")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)"), Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Desc { get; set; }

        /// <summary>
        ///是否强制升级
        /// </summary>
        [Display(Name = "是否强制升级")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false), Editable(true)]
        public bool Force { get; set; }

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