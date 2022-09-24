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
    [Entity(TableCnName = "群设置", TableName = "User_Group_Setting", DetailTable = new Type[] { typeof(User_Group_Member) }, DetailTableCnName = "群好友")]
    public class User_Group_Setting : BaseEntity
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
        [Display(Name = "GroupId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int GroupId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "Top")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool Top { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "是否免打扰")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool NotDisturb { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "SaveGroup")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool SaveGroup { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "AcceptAdd")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool AcceptAdd { get; set; }


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
        [Display(Name = "ModifyDate")]
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }


    }
}