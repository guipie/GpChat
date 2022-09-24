/*
*所有关于Sys_Setting类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using GuXin.Entity.AppDto.Input;

namespace GuXin.Business.IServices
{
    public partial interface ISys_SettingService
    {
        /// <summary>
        /// 获取app推荐配置系数
        /// </summary>
        /// <returns></returns>
        RecommendSettingDto GetReSettings();
        /// <summary>
        /// 修改/添加推荐配置系数
        /// </summary>
        /// <param name="settingDto"></param>
        /// <returns></returns>
        bool PostReSettings(RecommendSettingDto settingDto);
        /// <summary>
        /// 获取绑定配置系数
        /// </summary>
        /// <returns></returns>
        TopSettingDto GetTopSettings();
        /// <summary>
        /// 添加/修改榜单配置系数
        /// </summary>
        /// <param name="settingDto"></param>
        /// <returns></returns>
        bool PostTopSettings(TopSettingDto settingDto);
    }
}
