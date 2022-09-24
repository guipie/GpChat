using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuXin.Core.Filters
{
    public class AppJWTAuthorizeAttribute : AuthorizeAttribute
    {
        public AppJWTAuthorizeAttribute() : base()
        {

        }
    }
}
