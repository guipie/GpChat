/*
*所有关于Content_Intro类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System;
using GuXin.Entity.AppDto.Input;
using System.Collections.Generic;
using System.Linq;

namespace GuXin.Business.IServices
{
    public partial interface IContent_IntroService
    {
        /// <summary>
        /// 简介新增
        /// </summary>
        /// <param name="inputDtos"></param>
        /// <returns></returns>
        bool Post(List<ContentIntroInput> inputDtos);
        /// <summary>
        /// 简介修改
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        bool Put(ContentIntroInput inputDto);
        /// <summary>
        /// 简介删除
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        WebResponseContent Del(Guid guid);
        /// <summary>
        /// 简介点赞
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        WebResponseContent Praise(Guid guid);
        /// <summary>
        /// 获取用户简介
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        IQueryable<object> Get(int userId, DateTime searchDate);
    }
}
