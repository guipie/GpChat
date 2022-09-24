/*
*所有关于User_Tag类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using GuXin.Entity.AppDto.Input;
using System.Linq;

namespace GuXin.Business.IServices
{
    public partial interface IUser_TagService
    {
        WebResponseContent Post(TagDto tag);
        IQueryable<object> GetTags();
        IQueryable<object> GetMembers(int tagId);
    }
}
