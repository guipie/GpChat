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
    [Entity(TableCnName = "省市区",TableName = "Sys_Province")]
    public class Sys_Province:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="ProvinceId")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int ProvinceId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProvinceCode")]
       [MaxLength(40)]
       [Column(TypeName="nvarchar(40)")]
       [Required(AllowEmptyStrings=false)]
       public string ProvinceCode { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProvinceName")]
       [MaxLength(60)]
       [Column(TypeName="nvarchar(60)")]
       [Required(AllowEmptyStrings=false)]
       public string ProvinceName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="RegionCode")]
       [MaxLength(40)]
       [Column(TypeName="nvarchar(40)")]
       public string RegionCode { get; set; }

       
    }
}