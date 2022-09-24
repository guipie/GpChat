using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuXin.Business.IServices;
using GuXin.Core.Controllers.Basic;
using GuXin.Core.ObjectActionValidator;
using GuXin.Core.Utilities;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.AppEnum;
using GuXin.Core.ManageUser;
using System.IO;

namespace GuXin.App.Controllers
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
    [Route("User")]
    public partial class UserController : AppApiBaseController
    {
        private readonly IUserService _service;//访问业务代码
        private readonly IUser_XinPraiseService _user_XinPraiseService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="httpContextAccessor"></param>
        [ActivatorUtilitiesConstructor]
        public UserController(
            IUserService service,
            IUser_XinPraiseService user_XinPraiseService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _service = service;
            _user_XinPraiseService = user_XinPraiseService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> UserInfoAsync([FromRoute] int userId)
        {
            return OkData(await _service.UserInfo(userId));
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="phoneNo">手机号</param>
        /// <param name="code">验证码类别</param>
        /// <returns></returns>
        [HttpPost("Vcode/{phoneNo}"), AllowAnonymous]
        public Task<WebResponseContent> LCode([FromRoute] string phoneNo, [FromQuery] VCode code) => _service.GetVerifyCodeAsync(phoneNo, code);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="appLogin"></param>
        /// <returns></returns>
        [ObjectModelValidatorFilter(ValidatorModel.AppLogin)]
        [HttpPost("Login"), AllowAnonymous]
        public async Task<WebResponseContent> Login([FromBody] AppLogin appLogin) => await _service.LoginAsync(appLogin);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="rUser"></param>
        /// <returns></returns>
        [HttpPost("Register"), AllowAnonymous]
        public async Task<WebResponseContent> Register([FromBody] AppRegistereUser rUser) => await _service.RegisterAsync(rUser);
        /// <summary>
        /// token替换
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("Token/Replace")]
        public WebResponseContent ReplaceToken() => _service.ReplaceToken();

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="appLogin"></param>
        /// <returns></returns>
        [ObjectModelValidatorFilter(ValidatorModel.AppLogin)]
        [HttpPut("Password")]
        public WebResponseContent PwdReset([FromBody] AppLogin appLogin) => _service.SetPwd(appLogin);

        /// <summary>
        /// 手机号变更
        /// </summary>
        /// <param name="oldPhone">老手机号</param>
        /// <param name="appLogin"></param>
        /// <returns></returns>
        [ObjectModelValidatorFilter(ValidatorModel.AppLogin)]
        [HttpPut("Phone/{oldPhone}")]
        public WebResponseContent PhoneReset([FromRoute] string oldPhone, [FromBody] AppLogin appLogin) => _service.SetPhone(oldPhone, appLogin);
        /// <summary>
        /// 性别修改
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        [HttpPut("Sex/{sex}")]
        public WebResponseContent SexReset([FromRoute] int sex) => _service.SetSex(sex);

        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="nickName">昵称</param>
        /// <returns></returns>
        [HttpPut("NickName/{nickName}")]
        public async Task<WebResponseContent> NickNameResetAsync([FromRoute] string nickName) => await _service.SetNickNameAsync(nickName);

        /// <summary>
        /// 省市修改
        /// </summary>
        /// <param name="province">省</param>
        /// <param name="city">市</param>
        /// <returns></returns>
        [HttpPut("Location/{province}/{city}")]
        public WebResponseContent LocationReset([FromRoute] string province, [FromRoute] string city) => _service.SetCity(province, city);

        /// <summary>
        /// 备注介绍修改
        /// </summary>
        /// <param name="remark">介绍</param>
        /// <returns></returns>
        [HttpPut("Remark/{remark}")]
        public WebResponseContent RemarkReset([FromRoute] string remark) => _service.SetRemark(remark);

        /// <summary>
        /// 注销登出
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPut("Logout/{password}")]
        public WebResponseContent Logout([FromBody] string password) => _service.Logout(password);

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("Avatar/Upload")]
        public WebResponseContent AvatarUpload()
        {
            if (HttpContext.Request.Form.Files.Count == 0)
                return WebResponseContent.Instance.Error("后台未获取到上传头像");
            return _service.UploadAvatar(HttpContext.Request.Form.Files[0]);
        }
        /// <summary>
        /// 内容统计信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("AlbumInfo/{userId}")]
        public async Task<WebResponseContent> AlbumInfo(int userId)
        {
            userId = userId == 0 ? UserContext.Current.UserId : userId;
            return await _service.AlbumInfoAsync(userId);
        }

        [HttpGet("Avatar/{userId}"), AllowAnonymous]
        public async Task<IActionResult> Avatar([FromRoute] int userId)
        {
            using (var sw = new FileStream(await _service.Avatar(userId), FileMode.Open))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, "image/jpeg");
            }
        }
    }
}
