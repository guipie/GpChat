/*
 *所有关于User类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*UserService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using GuXin.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.Business.IRepositories;
using GuXin.Core.CacheManager;
using GuXin.Core.Configuration;
using GuXin.Core.ManageUser;
using GuXin.Entity.AppDto.Input;
using GuXin.Entity.AppDto.Output;
using GuXin.Core.Enums;
using GuXin.Core.Services;
using System;
using System.Threading.Tasks;
using GuXin.Entity.AppEnum;
using System.Collections.Concurrent;
using System.Collections.Generic;
using GuXin.Business.IServices;
using System.Linq;

namespace GuXin.Business.Services
{
    public partial class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _repository;//访问数据库

        private readonly IUser_FollowRepository _FollowRepository;//访问数据库
        private readonly ICacheService _cacheService;
        private readonly IUser_XinPraiseService _XinPraiseService;
        [ActivatorUtilitiesConstructor]
        public UserService(
            IUserRepository dbRepository,
            IHttpContextAccessor httpContextAccessor, ICacheService cacheService, IUser_FollowRepository followRepository,
            IUser_XinPraiseService xinPraiseService
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _FollowRepository = followRepository;
            _cacheService = cacheService;
            _XinPraiseService = xinPraiseService;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }

        private void SetToken(ref WebResponseContent responseContent, Sys_User user)
        {
            user.LastLoginDate = DateTime.Now;
            string token = JwtHelper.IssueJwt(new UserInfo()
            {
                User_Id = user.User_Id,
                UserName = user.UserName,
                Role_Id = user.Role_Id
            });
            var appUserInfo = new AppUserInfo()
            {
                UserId = user.User_Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Token = user.Token = token,
                Phone = user.PhoneNo,
                Avatar = user.Avatar,
                Sex = user.Gender,
                Province = user.Province,
                City = user.City,
                Remark = user.Remark,
                LastLoginDate = user.LastLoginDate
            };
            responseContent.Data = appUserInfo;
            _repository.Update(user, x => new { x.Token, x.LastLoginDate }, true);
            UserContext.Current.LogOut(appUserInfo.UserId);
        }
        public async Task<WebResponseContent> GetVerifyCodeAsync(string phoneNo, VCode vcode)
        {
            WebResponseContent webResponse = WebResponseContent.Instance.OK();
            try
            {
                if (vcode == 0) return webResponse.Error("未获取到验证码类别");
                if (vcode == VCode.Rcode && await _repository.ExistsAsync(m => m.PhoneNo == phoneNo))
                    return webResponse.Error("该手机号已被注册");
                if ((vcode == VCode.Lcode || vcode == VCode.Ucode) && !await _repository.ExistsAsync(m => m.PhoneNo == phoneNo))
                    return webResponse.Error("该手机号暂未注册");

                var (success, code) = await AliyunManage.SendRegisterSmsAsync(phoneNo);
                if (success)
                { _cacheService.Add(phoneNo, code, 60); return webResponse.OK(); }
                return webResponse.Error("一个小时后重试.");
            }
            catch (Exception ex)
            {
                Logger.Error(LoggerType.Login, phoneNo, "登录验证码", ex);
                return webResponse.Error(ResponseType.ServerError);
            }
            finally
            {
                Logger.Info(LoggerType.Login, phoneNo, "登录验证码");
            }
        }

        public async Task<WebResponseContent> LoginAsync(AppLogin lUser)
        {
            WebResponseContent responseContent = new();
            try
            {
                //验证码登录
                if (lUser.Vcode.IsNotNullOrEmpty() && lUser.Vcode > 0)
                {
                    var cacheCode = _cacheService.Get(lUser.PhoneNo).ToInt();
                    if (cacheCode.IsNullOrEmpty())
                        return responseContent.Error("验证码已失效");
                    if (cacheCode != lUser.Vcode)
                        return responseContent.Error("验证码不正确");
                    _cacheService.Remove(lUser.PhoneNo);
                }
                Sys_User sys_User = await _repository.DbContext.Set<Sys_User>().SingleOrDefaultAsync(x => x.PhoneNo == lUser.PhoneNo);
                if (sys_User == null)
                    return responseContent.Error("您还未注册.");

                if (lUser.PassWord.IsNotNullOrEmpty() && lUser.PassWord.Trim() != (sys_User.UserPwd ?? "").DecryptDES(AppSetting.Secret.User))
                    return responseContent.Error(ResponseType.LoginError, "密码错误");
                if (sys_User.LastLoginDate.DayOfYear != DateTime.Now.DayOfYear)
                    await _XinPraiseService.PostAsync(XinPraiseType.Login, sys_User.User_Id);
                SetToken(ref responseContent, sys_User);
                return responseContent.OK(ResponseType.LoginSuccess);
            }
            catch (Exception ex)
            {
                Logger.Error(LoggerType.Login, "登录错误", lUser.Serialize(), ex);
                return responseContent.Error(ResponseType.ServerError);
            }
            finally
            {
                Logger.Info(LoggerType.Login, lUser.Serialize(), responseContent.Message);
            }
        }

        public async Task<WebResponseContent> RegisterAsync(AppRegistereUser rUser)
        {

            WebResponseContent responseContent = new();
            try
            {
                if (rUser.InCode.IsNotNullOrEmpty())
                    if (rUser.InCode.Length < 4 || !_repository.Exists(m => m.UserName == rUser.InCode))
                        return responseContent.Error("无此邀请码");
                //验证码
                if (rUser.Vcode.IsNotNullOrEmpty() && rUser.Vcode > 0)
                {
                    var cacheCode = _cacheService.Get(rUser.PhoneNo).ToInt();
                    if (cacheCode.IsNullOrEmpty())
                        return responseContent.Error("验证码已失效");
                    if (cacheCode != rUser.Vcode)
                        return responseContent.Error("验证码不正确");
                    _cacheService.Remove(rUser.PhoneNo);
                }
                Sys_User sys_User = await _repository.DbContext.Set<Sys_User>().SingleOrDefaultAsync(x => x.PhoneNo == rUser.PhoneNo);
                if (sys_User != null)
                    return responseContent.Error("您已经注册过了.");
                _repository.DbContextBeginTransaction(() =>
                {
                    sys_User = new Sys_User()
                    {
                        UserName = StringExtension.GenerateRandomNumber(10),
                        PhoneNo = rUser.PhoneNo,
                        NickName = "GuXiner" + DateTime.Now.Millisecond,
                        RoleName = "App用户",
                        Role_Id = 0,
                        LastLoginDate = DateTime.Now,
                        Enable = UserStatus.Enable
                    };
                    sys_User.SetCreateDefaultVal();
                    _repository.Add(sys_User, true);
                    return responseContent.Info(sys_User.User_Id);
                });
                if (!responseContent.Status) return responseContent;

                SetToken(ref responseContent, sys_User);
                await _XinPraiseService.PostAsync(XinPraiseType.Login, sys_User.User_Id);
                if (rUser.InCode.IsNotNullOrEmpty()) //邀请用户 送新赞
                {
                    var inviteUserId = _repository.FindFirst(m => m.UserName == rUser.InCode).User_Id;
                    await _XinPraiseService.PostAsync(XinPraiseType.Login, inviteUserId);
                }
                _repository.DbContext.Set<User_Friend>().Add(new User_Friend()
                {
                    CreateDate = DateTime.Now,
                    CreateID = sys_User.User_Id,
                    Creator = sys_User.UserName,
                    FriendRemarkName = sys_User.NickName,
                    FriendUserId = sys_User.User_Id,
                    Status = FriendStatus.Friend,
                    Top = false,
                    NotDisturb = false,
                    Description = "注册添加"
                });
                _repository.SaveChanges();
                return responseContent.OK(ResponseType.RegisterSuccess);
            }
            catch (Exception ex)
            {
                Logger.Error(LoggerType.Register, "注册错误", rUser.Serialize(), ex);
                return responseContent.Error(ResponseType.ServerError);
            }
            finally
            {
                Logger.Info(LoggerType.Register, rUser.Serialize(), responseContent.Message);
            }
        }

        private readonly ConcurrentDictionary<int, object> _lockCurrent = new ConcurrentDictionary<int, object>();
        public WebResponseContent ReplaceToken()
        {
            WebResponseContent responseContent = WebResponseContent.Instance.OK();
            string key = $"rp:Token:{UserContext.Current.UserId}";
            try
            {
                //如果5秒内替换过token,直接使用最新的token(防止一个页面多个并发请求同时替换token导致token错位)
                if (_cacheService.Exists(key))
                    return responseContent.OK(null, _cacheService.Get(key));

                var _obj = _lockCurrent.GetOrAdd(UserContext.Current.UserId, new object() { });
                lock (_obj)
                {
                    if (_cacheService.Exists(key))
                        return responseContent.OK(null, _cacheService.Get(key));

                    string requestToken = _httpContextAccessor.HttpContext.Request.Headers[AppSetting.TokenHeaderName];
                    requestToken = requestToken?.Replace("Bearer ", "");

                    if (JwtHelper.IsExp(requestToken)) return responseContent.Error("登录已过期!");

                    int userId = UserContext.Current.UserId;

                    var userInfo = _repository.FindFirst(x => x.User_Id == userId && x.Enable == UserStatus.Enable);

                    if (userInfo == null) return responseContent.Error("未查到您的用户信息!");

                    SetToken(ref responseContent, userInfo);
                    if (responseContent.Status)
                    {
                        var currentToken = responseContent.Data.ToString();
                        //移除当前缓存
                        _cacheService.Remove(userId.GetUserIdKey());
                        //添加一个5秒缓存
                        _cacheService.Add(key, currentToken, 5);
                        _ = _XinPraiseService.PostAsync(XinPraiseType.Login);
                        return responseContent;
                    }
                    else
                        return responseContent.Error("出现异常,重新登录.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(LoggerType.UserToken, ex.Message, null, ex);
                return responseContent.Error("出现异常,重新登录");
            }
            finally
            {
                _lockCurrent.TryRemove(UserContext.Current.UserId, out object val);
            }
        }
        public WebResponseContent SetSex(int sex) => WebResponseContent.Instance.Info(_repository.Update(new Sys_User() { User_Id = UserContext.Current.UserId, Gender = sex }, m => m.Gender, true), "性别修改成功");

        public WebResponseContent SetPwd(AppLogin appLogin)
        {
            WebResponseContent webResponse = new();
            if (appLogin.PassWord != appLogin.ConfirmPassWord)
                return webResponse.Error("两次密码不一致");
            //验证码 
            var cacheCode = _cacheService.Get(appLogin.PhoneNo).ToInt();
            if (cacheCode.IsNullOrEmpty())
                return webResponse.Error("验证码已失效");
            if (cacheCode != appLogin.Vcode)
                return webResponse.Error("验证码不正确");
            _cacheService.Remove(appLogin.PhoneNo);
            var user = _repository.FindFirst(m => m.PhoneNo == appLogin.PhoneNo);
            if (user == null)
                return webResponse.Error("该手机用户不存在");
            if (user.User_Id != UserContext.Current.UserId)
                return webResponse.Error("您没有权限重置");
            user.UserPwd = appLogin.PassWord.EncryptDES(AppSetting.Secret.User);
            return webResponse.Info(_repository.Update(user, m => m.UserPwd, true));
        }

        public WebResponseContent SetPhone(string oldPhone, AppLogin appLogin)
        {
            WebResponseContent webResponse = new();
            //验证码 
            var cacheCode = _cacheService.Get(appLogin.PhoneNo).ToInt();
            if (cacheCode.IsNullOrEmpty())
                return webResponse.Error("验证码已失效");
            if (cacheCode != appLogin.Vcode)
                return webResponse.Error("验证码不正确");
            _cacheService.Remove(appLogin.PhoneNo);

            var user = _repository.FindFirst(m => m.PhoneNo == appLogin.PhoneNo);
            if (user != null)
                return webResponse.Error("该手机用户已存在");

            var oldUser = _repository.FindFirst(m => m.PhoneNo == oldPhone);
            if (oldUser == null || oldUser.User_Id != UserContext.Current.UserId || oldUser.Enable != GuXin.Entity.AppEnum.UserStatus.Enable)
                return webResponse.Error("您没有权限重置");

            user.PhoneNo = appLogin.PhoneNo;
            _repository.Update(user, m => m.PhoneNo);
            return webResponse.Info(repository.SaveChanges());
        }

        public WebResponseContent Logout(string password)
        {
            var user = _repository.FindFirst(m => m.User_Id == UserContext.Current.UserId && m.UserPwd == password.DecryptDES(AppSetting.Secret.User));
            if (user == null) return WebResponseContent.Instance.Error("登录后重试");
            user.Enable = UserStatus.Logout;
            return WebResponseContent.Instance.Info(_repository.Update(user, m => m.Enable, true));
        }
        public async Task<WebResponseContent> SetNickNameAsync(string nickName)
        {
            var user = await _repository.FindAsIQueryable(m => m.User_Id == UserContext.Current.UserId).AsTracking().FirstOrDefaultAsync();
            if (user == null) return WebResponseContent.Instance.Error("登录后重试");
            user.NickName = nickName;
            return WebResponseContent.Instance.Info(await _repository.SaveChangesAsync(), "昵称设置成功");
        }
        public WebResponseContent SetCity(string province, string city)
        {
            var user = _repository.FindFirst(m => m.User_Id == UserContext.Current.UserId);
            if (user == null) return WebResponseContent.Instance.Error("登录后重试");
            user.Province = province;
            user.City = city;
            return WebResponseContent.Instance.Info(_repository.Update(user, m => new { m.Province, m.City }, true), "地区设置成功");
        }
        public WebResponseContent SetRemark(string remark)
        {
            var user = _repository.FindFirst(m => m.User_Id == UserContext.Current.UserId);
            if (user == null) return WebResponseContent.Instance.Error("登录后重试");
            user.Remark = remark;
            return WebResponseContent.Instance.Info(_repository.Update(user, m => new { m.Remark }, true), "签名设置成功");
        }

        public WebResponseContent UploadAvatar(IFormFile file)
        {
            WebResponseContent content = Upload(new List<IFormFile>() { file }, "Avatar/" + DateTime.Now.ToString("yyyyMM"));
            if (content.Status)
            {
                _repository.Update(new Sys_User() { Avatar = content.Data.ToString() + file.FileName, User_Id = UserContext.Current.UserId }, m => m.Avatar, true);
                content.Data = content.Data.ToString() + file.FileName;
            }
            return content;
        }
        public async Task<WebResponseContent> AlbumInfoAsync(int userId)
        {
            var userInfo = _repository.FindFirst(m => m.User_Id == userId && m.Enable == UserStatus.Enable);
            if (userInfo == null) return WebResponseContent.Instance.Error("该用户已经注销");
            var data = new
            {
                UserId = userInfo.User_Id,
                IsMine = userInfo.User_Id == UserContext.Current.UserId,
                userInfo.NickName,
                userInfo.Remark,
                userInfo.Avatar,
                FollowCount = await _repository.DbContext.Set<User_Follow>().CountAsync(m => m.CreateID == userId),
                FollowedCount = await _repository.DbContext.Set<User_Follow>().CountAsync(m => m.FollowUserId == userId),
                isFollowing = _FollowRepository.Exists(x => x.FollowUserId == userInfo.User_Id && x.CreateID == UserContext.Current.UserId)
            };
            return WebResponseContent.Instance.OK("用户主页获取成功", data);
        }
        public async Task<string> Avatar(int userId)
        {
            var path = (await _repository.FindAsIQueryable(m => m.User_Id == userId).FirstOrDefaultAsync()).Avatar;
            return path.MapPath(true);
        }
        public async Task<object> UserInfo(int userId)
        {
            return await _repository.FindAsIQueryable(m => m.User_Id == userId).Select(m => new { m.Avatar, m.NickName, UserId = m.User_Id, m.Remark }).FirstOrDefaultAsync();
        }
    }
}
