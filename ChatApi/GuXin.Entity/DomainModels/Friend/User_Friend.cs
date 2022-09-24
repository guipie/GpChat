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
    [Entity(TableCnName = "好友管理", TableName = "User_Friend")]
    public class User_Friend : BaseEntity
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
        ///用户好友ID
        /// </summary>
        [Display(Name = "用户好友ID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int FriendUserId { get; set; }

        /// <summary>
        ///好友备注
        /// </summary>
        [Display(Name = "好友备注")]
        [MaxLength(40)]
        [Column(TypeName = "nvarchar(40)")]
        public string FriendRemarkName { get; set; }

        /// <summary>
        ///好友状态(0待同意，1同意，2拒绝，3，拉黑)
        /// </summary>
        [Display(Name = "好友状态(0待同意，1同意，2拒绝，3，拉黑)")]
        [Column(TypeName = "smallint")]
        [Required(AllowEmptyStrings = false)]
        public FriendStatus Status { get; set; }

        /// <summary>
        ///添加好友描述
        /// </summary>
        [Display(Name = "添加好友描述")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string Description { get; set; }

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
        [Display(Name = "NotDisturb")]
        [Column(TypeName = "bit")]
        [Required(AllowEmptyStrings = false)]
        public bool NotDisturb { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "BackgroundImage")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string BackgroundImage { get; set; }

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

        /// <summary>
        /// 好友头像
        /// </summary>
        [NotMapped]
        public string Avatar { get; set; }

        [NotMapped]
        public string FriendUserName { get; set; }

        [NotMapped]
        public string PinYin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="avatar"></param>
        public User_Friend Init(User_Friend source, string avatar)
        {
            source.Avatar = avatar;
            return source;
        }
        public User_Friend Init(User_Friend source, string avatar, string nickName)
        {
            source.Avatar = avatar;
            source.FriendRemarkName = nickName;
            return source;
        }
    }
}