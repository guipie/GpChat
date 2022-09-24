using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;

namespace GuXin.Core.Filters.AppFilters
{
    /// <summary>
    /// 检查头
    /// </summary>
    public class ValidateReferrerAttribute : ActionFilterAttribute
    {
        private IConfiguration _configuration;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));

            base.OnActionExecuting(context);

            if (!IsValidRequest(context.HttpContext.Request))
            {
                context.Result = new ContentResult
                {
                    Content = $"Invalid referer header",
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NetworkAuthenticationRequired;
            }
        }
        private bool IsValidRequest(HttpRequest request)
        {
            string referrerURL = "";

            if (request.Headers.ContainsKey("Referer"))
            {
                referrerURL = request.Headers["Referer"];
            }
            if (string.IsNullOrWhiteSpace(referrerURL)) return true;

            var allowedUrls = _configuration.GetSection("CorsOrigin").Get<string[]>()?.Select(url => new Uri(url).Authority).ToList();

            bool isValidClient = allowedUrls.Contains(new Uri(referrerURL).Authority);

            return isValidClient;
        }
    }
}
