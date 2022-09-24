
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GuXin.Core.CacheManager;
using GuXin.Core.Configuration;
using GuXin.Core.Controllers.Basic;
using GuXin.Core.DBManager;
using GuXin.Core.EFDbContext;
using GuXin.Core.Enums;
using GuXin.Core.Extensions;
using GuXin.Core.Filters;
using GuXin.Core.Infrastructure;
using GuXin.Core.ManageUser;
using GuXin.Core.ObjectActionValidator;
using GuXin.Core.Services;
using GuXin.Core.Utilities;
using GuXin.Entity.AttributeManager;
using GuXin.Entity.DomainModels;
using GuXin.System.IRepositories;
using GuXin.System.IServices;
using GuXin.System.Repositories;

namespace GuXin.System.Controllers
{
    [Route("api/user/statistic")]
    public partial class Sys_UserController
    {
        /// <summary>
        /// 今日数据统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("today"), ApiActionPermission]
        public IActionResult TodayData()
        {
            return Json(Service.GetTodayStatistic());
        }
        /// <summary>
        /// 本周数据统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("week"), ApiActionPermission]
        public IActionResult WeekData()
        {
            return Json(Service.GetWeekStatistic());
        }
        /// <summary>
        /// 总数据统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("all"), ApiActionPermission]
        public IActionResult AllData()
        {
            return Json(Service.GetStatistic());
        }
        [HttpGet("user/top5"), ApiActionPermission]
        public IActionResult Top5User()
        {
            return Json(Service.GetUserTop5());
        }
    }
}
