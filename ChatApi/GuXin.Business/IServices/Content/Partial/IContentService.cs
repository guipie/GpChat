/*
*所有关于Content类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;
using GuXin.Entity.AppDto.Output;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuXin.Business.IServices
{
    /// <summary>
    /// 内容列表
    /// </summary>
    public partial interface IContentService
    {
        /// <summary>
        /// 内容详情
        /// </summary>
        /// <param name="contentGuid"></param>
        /// <returns></returns>
        Task<WebResponseContent> Detail(Guid contentGuid);
        /// <summary>
        /// 初始化最新内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        Task<List<AppContent>> InitContentsAsync(DateTime searchDate);
        /// <summary>
        /// 推荐内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        Task<List<AppContent>> RecommendContentsAsync(DateTime searchDate);
        /// <summary>
        /// 榜单内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        Task<List<AppContent>> TopContentsAsync(DateTime searchDate);

        /// <summary>
        /// 我的互动内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="type">0全部，1点赞，2评论，3新赞</param>
        /// <returns>内容详情列表</returns>
        Task<IEnumerable<object>> InteractContentsAsync(DateTime searchDate, int type);
        /// <summary>
        /// 搜索故新内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<IEnumerable<object>> SearchContentsAsync(DateTime searchDate, string key);
        /// <summary>
        /// 搜索故新用户内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<IEnumerable<object>> SearchUserContentsAsync(DateTime searchDate, string key, int? userId);
        /// <summary>
        /// 收藏内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        IList<AppContent> CollectContents(DateTime searchDate);
        /// <summary>
        /// 关注的内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        Task<List<AppContent>> FollowContentsAsync(DateTime searchDate, int userId = 0);
        /// <summary>
        /// 用户朋友圈内容
        /// </summary>
        /// <param name="searchDate"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<FriendContent>> FriendContentsAsync(DateTime searchDate, int userId = 0);
        /// <summary>
        /// 朋友圈相册
        /// </summary>
        /// <param name="year"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        object FriendPics(DateTime date);
        /// <summary>
        /// 用户内容统计
        /// </summary> 
        /// <returns></returns>
        (int, int, int) Statistic();
    }
}
