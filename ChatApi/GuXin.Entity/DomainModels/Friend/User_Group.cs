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
    [Entity(TableCnName = "聊天群", TableName = "User_Group")]
    public class User_Group : BaseEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "GroupId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int GroupId { get; set; }

        /// <summary>
        ///群名称
        /// </summary>
        [Display(Name = "群名称")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        /// <summary>
        ///q群描述
        /// </summary>
        [Display(Name = "标签描述")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string Description { get; set; }

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
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
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



        [Display(Name = "群成功")]
        [ForeignKey("GroupId")]
        public List<User_Group_Member> Members { get; set; }
    }
}