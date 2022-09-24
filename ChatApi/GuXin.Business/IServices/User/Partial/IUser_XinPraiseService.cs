/*
*所有关于User_XinPraise类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using GuXin.Entity.AppEnum;
using System.Threading.Tasks;
using GuXin.Entity.AppDto.Output;
using System;

namespace GuXin.Business.IServices
{
    public partial interface IUser_XinPraiseService
    {
        User_XinPraise Get();
        UserXinZanInfo GetInfo();
        Task<int> PostAsync(XinPraiseType xinPraiseType, int userId = 0);
         
    }
}
