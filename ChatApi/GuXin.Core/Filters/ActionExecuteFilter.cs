using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using GuXin.Core.Enums;
using GuXin.Core.Extensions;
using GuXin.Core.ObjectActionValidator;
using GuXin.Core.Services;
using GuXin.Core.Utilities;

namespace GuXin.Core.Filters
{
    public class ActionExecuteFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //验证方法参数
            context.ActionParamsValidator();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}