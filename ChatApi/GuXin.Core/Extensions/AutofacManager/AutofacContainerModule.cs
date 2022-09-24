using GuXin.Core.Extensions;
using System;
using GuXin.Core.Configuration;

namespace GuXin.Core.Extensions.AutofacManager
{
    public class AutofacContainerModule
    {
        public static TService GetService<TService>() where TService:class
        {
            return typeof(TService).GetService() as TService;
        }
    }
}
