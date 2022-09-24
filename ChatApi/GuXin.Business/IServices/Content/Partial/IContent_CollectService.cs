/*
*所有关于Content_Collect类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System;
using System.Linq;
using GuXin.Entity.AppDto.Output;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GuXin.Business.IServices
{
    public partial interface IContent_CollectService
    {

        WebResponseContent PostCollect(Guid contentGuid);
        //Task<IList<AppContent>> GetMyCollects(DateTime searchDate);
    }
}
