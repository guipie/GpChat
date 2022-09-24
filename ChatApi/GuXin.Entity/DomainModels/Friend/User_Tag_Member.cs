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
    [Entity(TableCnName = "标签好友", TableName = "User_Tag_Member")]
    public class User_Tag_Member : BaseEntity
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
        [Display(Name = "TagName")]
        [Column(TypeName = "nvarchar(20)")]
        [Required(AllowEmptyStrings = false)]
        public string TagName { get; set; }
        /// <summary>
        ///
        /// </summary>
        [Display(Name = "TagId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int TagId { get; set; }

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
        [Display(Name = "UserNickName")]
        [Column(TypeName = "nvarchar(20)")]
        [Required(AllowEmptyStrings = false)]
        public string UserNickName { get; set; }

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