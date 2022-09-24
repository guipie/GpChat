/*
*所有关于Sys_Agreement类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using GuXin.Entity.AppEnum;

namespace GuXin.Business.IServices
{
    public partial interface ISys_AgreementService
    {
        Sys_Agreement AgreementInfo(Agreement agreement);
    }
}
