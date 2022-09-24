/*
*所有关于Sys_Province类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;

namespace GuXin.System.IServices
{
    public partial interface ISys_ProvinceService
    {
        IQueryable<object> GetProvinces();
        IQueryable<object> GetCities(string pCode);
    }
}
