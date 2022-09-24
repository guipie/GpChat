/*
*所有关于App_Upgrade类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
namespace GuXin.Business.IServices
{
    public partial interface IApp_UpgradeService
    {
        WebResponseContent App_Upgrade(string platform, string version);
    }
}
