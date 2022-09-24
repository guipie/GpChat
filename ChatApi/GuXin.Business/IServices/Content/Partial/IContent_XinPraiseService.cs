/*
*所有关于Content_XinPraise类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace GuXin.Business.IServices
{
    public partial interface IContent_XinPraiseService
    {
        WebResponseContent PostContentXinZan(Guid contentGuid);
        IQueryable<object> GetXinPraises(Guid contentGuid, DateTime searchDate);
    }
}
