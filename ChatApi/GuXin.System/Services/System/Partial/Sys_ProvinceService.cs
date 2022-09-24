/*
 *所有关于Sys_Province类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Sys_ProvinceService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using System.Linq;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using GuXin.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.System.IRepositories;

namespace GuXin.System.Services
{
    public partial class Sys_ProvinceService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISys_ProvinceRepository _repository;//访问数据库
        private readonly ISys_CityRepository _CityRepository;//访问数据库

        [ActivatorUtilitiesConstructor]
        public Sys_ProvinceService(
            ISys_ProvinceRepository dbRepository,
            ISys_CityRepository cityRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _CityRepository = cityRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        public IQueryable<object> GetCities(string pCode)
        {
            return _CityRepository.FindAsIQueryable(m => m.ProvinceCode == pCode).OrderBy(m => m.CityCode).Select(m => new { m.CityCode, m.CityName });
        }

        public IQueryable<object> GetProvinces()
        {
            return _repository.FindAsIQueryable(m => true).OrderBy(m => m.ProvinceCode).Select(m => new { m.ProvinceCode, m.ProvinceName });
        }
    }
}
