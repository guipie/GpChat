/*
*所有关于Content_Praise类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace GuXin.Business.IServices
{
    public partial interface IContent_PraiseService
    {
        WebResponseContent PostPraise(Guid contentGuid);
        WebResponseContent PostFriendPraise(Guid contentGuid);
        /// <summary>
        /// 获取内容点赞用户
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        IQueryable<object> GetPraiseUsers(Guid contentGuid, DateTime searchDate);
        IQueryable<object> GetFriendPraiseUsers(Guid contentGuid, DateTime searchDate);
    }
}
