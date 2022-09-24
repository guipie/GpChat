/*
*所有关于Content_XinPraise类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System;
using System.Linq;
using GuXin.Entity.AppDto.Input;
using System.Threading.Tasks;
using GuXin.Entity.AppDto.Output;
using System.Collections.Generic;

namespace GuXin.Business.IServices
{
    /// <summary>
    /// 用户内容操作
    /// </summary>
    public partial interface IContentService
    {

        /// <summary>
        /// 用户公共内容
        /// </summary>
        /// <returns></returns>
        Task<List<AppContent>> GetAsync(int userId, DateTime searchDate);
        /// <summary>
        /// 用户公共优质内容
        /// </summary>
        /// <returns></returns>
        Task<List<AppContent>> GetTopAsync(int userId, DateTime searchDate);

        /// <summary>
        ///  用户内容新增
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        WebResponseContent Post(ContentInput inputDto);
        /// <summary>
        /// 内容修改
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        bool Put(ContentInput inputDto);
        /// <summary>
        ///  用户内容删除
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        WebResponseContent Del(Guid guid);
        /// <summary>
        ///  用户编辑内容详情
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        ContentInput EditDetail(Guid guid);
    }
}
