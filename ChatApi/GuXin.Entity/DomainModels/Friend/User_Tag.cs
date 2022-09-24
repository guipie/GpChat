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
    [Entity(TableCnName = "标签", TableName = "User_Tag", DetailTable = new Type[] { typeof(User_Tag_Member) }, DetailTableCnName = "标签成员")]
    public class User_Tag : BaseEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "TagId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int TagId { get; set; }

        /// <summary>
        ///标签名称
        /// </summary>
        [Display(Name = "标签名称")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        ///标签描述
        /// </summary>
        [Display(Name = "标签描述")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string Description { get; set; }


        [Display(Name = "成员个数")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int MemberCount { get; set; }
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
        [Display(Name = "Modifier")]
        [MaxLength(60)]
        [Column(TypeName = "nvarchar(60)")]
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

        [Display(Name = "标签成员")]
        [ForeignKey("TagId")]
        public List<User_Tag_Member> User_Tag_Member { get; set; }

    }
}