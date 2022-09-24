/*
*所有关于User类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.AppEnum;
using Microsoft.AspNetCore.Http;

namespace GuXin.Business.IServices
{
    public partial interface IUserService
    {
        /// <summary>
        /// 获取登录验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns></returns>
        Task<WebResponseContent> GetVerifyCodeAsync(string phoneNo, VCode code);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="lUser">登录用户</param>
        /// <returns></returns>
        Task<WebResponseContent> LoginAsync(AppLogin lUser);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="lUser">注册用户</param>
        /// <returns></returns>
        Task<WebResponseContent> RegisterAsync(AppRegistereUser rUser);

        WebResponseContent ReplaceToken();

        WebResponseContent SetSex(int sex);
        WebResponseContent SetPwd(AppLogin appLogin);
        WebResponseContent SetPhone(string oldPhone, AppLogin appLogin);
        Task<WebResponseContent> SetNickNameAsync(string nickName);
        WebResponseContent SetCity(string province, string city);
        WebResponseContent SetRemark(string remark);
        WebResponseContent Logout(string password);

        WebResponseContent UploadAvatar(IFormFile file);

        Task<WebResponseContent> AlbumInfoAsync(int userId);

        Task<string> Avatar(int userId);
        Task<object> UserInfo(int userId);
    }
}
