/*
 *所有关于Sys_Setting类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Sys_SettingService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
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
using GuXin.Business.IRepositories;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.AppEnum;

namespace GuXin.Business.Services
{
    public partial class Sys_SettingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISys_SettingRepository _repository;//访问数据库

        [ActivatorUtilitiesConstructor]
        public Sys_SettingService(
            ISys_SettingRepository dbRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }
        public RecommendSettingDto GetReSettings()
        {
            var model = _repository.FindFirst(m => m.DicNo == SysSetting.recommend);
            if (model?.Id > 0)
                return model.SettingsJson.DeserializeObject<RecommendSettingDto>();
            return new RecommendSettingDto();
        }

        public bool PostReSettings(RecommendSettingDto settingDto)
        {
            var entity = new Sys_Setting() { DicNo = SysSetting.recommend, SettingsJson = settingDto.ToJson() }.SetCreateDefaultVal();
            var model = _repository.FindFirst(m => m.DicNo == SysSetting.recommend);
            if (model?.Id > 0)
                _repository.Update(entity, true);
            else
                _repository.Add(entity, true);
            return true;
        }
        public TopSettingDto GetTopSettings()
        {
            var model = _repository.FindFirst(m => m.DicNo == SysSetting.top);
            if (model?.Id > 0)
                return model.SettingsJson.DeserializeObject<TopSettingDto>();
            return new TopSettingDto();
        }

        public bool PostTopSettings(TopSettingDto settingDto)
        {
            var entity = new Sys_Setting() { DicNo = SysSetting.top, SettingsJson = settingDto.ToJson() }.SetCreateDefaultVal();
            var model = _repository.FindFirst(m => m.DicNo == SysSetting.top);
            if (model?.Id > 0)
                _repository.Update(entity, true);
            else
                _repository.Add(entity, true);
            return true;
        }
    }
}
