/*
*所有关于Content_File类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace GuXin.Business.IServices
{
    public partial interface IContent_FileService
    {
        IQueryable<Content_File> GetFiles(Guid contentGuid);
    }
 }
