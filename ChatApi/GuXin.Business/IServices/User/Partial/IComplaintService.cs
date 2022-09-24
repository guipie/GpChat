/*
*所有关于Complaint类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GuXin.Business.IServices
{
    public partial interface IComplaintService
    {
        WebResponseContent Post(Complaint complaint);


    }
}
