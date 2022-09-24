using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using GuXin.Core.Configuration;
using GuXin.Core.Extensions;
using GuXin.Core.ManageUser;
using GuXin.Core.Utilities;

namespace GuXin.Core.Filters
{
    public interface IFixedTokenFilter : IFilterMetadata
    {
        AuthorizationFilterContext OnAuthorization(AuthorizationFilterContext context);
    }
    public class FixedTokenAttribute : Attribute, IFixedTokenFilter, IAllowAnonymous
    {
        public AuthorizationFilterContext OnAuthorization(AuthorizationFilterContext context)
        {
            string fixedoken = "";
            if (context.ActionDescriptor.EndpointMetadata.Any(item => item is AllowAnonymousAttribute))
                return context;
            //如果token已失效，直接获取header里的token
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                fixedoken = context.HttpContext.Request.Headers[AppSetting.TokenHeaderName];
                fixedoken = fixedoken?.Replace("Bearer ", "");
                //判断是否传入了token
                if (string.IsNullOrEmpty(fixedoken))
                {
                    return context.Unauthorized("没有传入token");
                }
                //解析token
                int userId = JwtHelper.GetUserId(fixedoken);
                if (userId <= 0)
                {
                    return context.Unauthorized("token不正确");
                }
                context.AddIdentity(userId);
            }
            else
            {
                fixedoken = ((ClaimsIdentity)context.HttpContext.User.Identity)
                ?.BootstrapContext?.ToString();
            }
            //判断当前用户的token与缓存的token是否相同
            if (UserContext.Current.Token != fixedoken)
            {
                context.FilterResult(HttpStatusCode.Unauthorized, "token已失效");
            }
            return context;
        }
    }
}
