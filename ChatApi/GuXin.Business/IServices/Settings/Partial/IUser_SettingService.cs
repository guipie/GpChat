/*
*所有关于User_Setting类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
namespace GuXin.Business.IServices
{
    /// <summary>
    /// 当前用户故新管理相关配置
    /// </summary>
    public partial interface IUser_SettingService
    {
        /// <summary>
        /// 当前用户给故新管理配置
        /// </summary>
        /// <param name="halfYear">是否半年可见</param>
        /// <param name="followReply">是否关注后才能回复</param>
        /// <returns></returns>
        bool CurrentSetting(bool? halfYear, bool? followReply);
        /// <summary>
        /// 获取当前用户配置信息
        /// </summary>
        /// <returns></returns>
        User_Setting GetCurrentSetting();
    }
}
