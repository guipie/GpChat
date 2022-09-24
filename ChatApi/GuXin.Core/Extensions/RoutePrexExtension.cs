using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuXin.Core.Controllers;

namespace GuXin.Core.Extensions
{
    public static class RoutePrexExtension
    {
        public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Insert(0, new ControllersPrefix(routeAttribute));
        }
    }
}
