/*
*所有关于Content_Comment类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System;
using System.Linq;
using GuXin.Entity.AppDto.Output;
using System.Collections.Generic;

namespace GuXin.Business.IServices
{
    public partial interface IContent_CommentService
    {
        /// <summary>
        /// 添加内容评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Content_Comment PostComment(Guid contentGuid, string comments, int? parentGuid);
        Content_Comment PutComment(int commentsId, string comments);
        bool DelComment(int commentsId);
        int PraiseComment(int commentsId);

        /// <summary>
        /// 添加朋友圈评论
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Content_Comment PostFriendComment(Guid contentGuid, string comments, int? parentGuid);
        IList<ContentComent> GetComments(Guid contentGuid, DateTime searchDate);

        IQueryable<object> GetFriendComments(Guid contentGuid, DateTime searchDate);
        /// <summary>
        /// 点赞评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        WebResponseContent PutCommentPraise(int commentId);
    }
}
