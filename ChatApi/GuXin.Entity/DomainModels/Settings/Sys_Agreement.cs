using Newtonsoft.Json;
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
    [Entity(TableCnName = "App协议",TableName = "Sys_Agreement")]
    public class Sys_Agreement:BaseEntity
    {
        /// <summary>
       ///编号
       /// </summary>
       [Key]
       [Display(Name ="编号")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int Id { get; set; }

       /// <summary>
       ///app协议
       /// </summary>
       [Display(Name ="app协议")]
       [MaxLength(40)]
       [Column(TypeName="nvarchar(40)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string DicNo { get; set; }

       /// <summary>
       ///标题
       /// </summary>
       [Display(Name ="标题")]
       [MaxLength(40)]
       [Column(TypeName="nvarchar(40)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Title { get; set; }

       /// <summary>
       ///内容
       /// </summary>
       [Display(Name ="内容")]
       [MaxLength(16)]
       [Column(TypeName="text(16)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Html { get; set; }

       /// <summary>
       ///创建时间
       /// </summary>
       [Display(Name ="创建时间")]
       [Column(TypeName="datetime")]
       [Required(AllowEmptyStrings=false)]
       public DateTime CreateDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CreateID")]
       [JsonIgnore]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int CreateID { get; set; }

       /// <summary>
       ///创建人
       /// </summary>
       [Display(Name ="创建人")]
       [MaxLength(60)]
       [Column(TypeName="nvarchar(60)")]
       [Required(AllowEmptyStrings=false)]
       public string Creator { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="Modifier")]
       [MaxLength(60)]
       [JsonIgnore]
       [Column(TypeName="nvarchar(60)")]
       public string Modifier { get; set; }

       /// <summary>
       ///更新时间
       /// </summary>
       [Display(Name ="更新时间")]
       [Column(TypeName="datetime")]
       public DateTime? ModifyDate { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ModifyID")]
       [JsonIgnore]
       [Column(TypeName="int")]
       public int? ModifyID { get; set; }

       
    }
}
