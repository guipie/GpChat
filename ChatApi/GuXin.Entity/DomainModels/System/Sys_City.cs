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
    [Entity(TableCnName = "市",TableName = "Sys_City")]
    public class Sys_City:BaseEntity
    {
        /// <summary>
       ///
       /// </summary>
       [Key]
       [Display(Name ="CityId")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int CityId { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CityCode")]
       [MaxLength(40)]
       [Column(TypeName="nvarchar(40)")]
       public string CityCode { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="CityName")]
       [MaxLength(60)]
       [Column(TypeName="nvarchar(60)")]
       public string CityName { get; set; }

       /// <summary>
       ///
       /// </summary>
       [Display(Name ="ProvinceCode")]
       [MaxLength(20)]
       [Column(TypeName="varchar(20)")]
       public string ProvinceCode { get; set; }

       
    }
}