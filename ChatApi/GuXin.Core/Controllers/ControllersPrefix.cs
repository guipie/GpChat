using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Core.Controllers
{
    public class ControllersPrefix : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _prefix;
        public ControllersPrefix(IRouteTemplateProvider routeTemplateProvider)
        {
            _prefix = new AttributeRouteModel(routeTemplateProvider);
        }
        public void Apply(ApplicationModel application)
        {
            //遍历所有的 Controller
            foreach (var controller in application.Controllers)
            {
                // 已经标记了 RouteAttribute 的 Controller
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        // 在当前路由上 再 添加一个路由前缀
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_prefix, selectorModel.AttributeRouteModel);
                    }
                }

                // 没有标记 RouteAttribute 的 Controller
                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        // 添加一个路由前缀
                        selectorModel.AttributeRouteModel = _prefix;
                    }
                }
            }
        }
    }
}
