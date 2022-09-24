using GuXin.Entity.AppDto.Common;
using GuXin.Entity.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.System.Services
{
    public partial class Sys_UserService
    {

        public object GetTodayStatistic()
        {
            return new
            {
                registeredCount = repository.FindAsIQueryable(m => m.CreateDate.Value.Date == DateTime.Now.Date).Count(),
                onlineCount = ImHelper.GetClientListByOnline().Count(),
                contentCount = repository.DbContext.Set<Content>().Count(m => m.CreateDate.Date == DateTime.Now.Date && m.IsPublic),
                friendContentCount = repository.DbContext.Set<Content>().Count(m => m.CreateDate.Date == DateTime.Now.Date && m.IsFriend)
            };
        }

        public object GetWeekStatistic()
        {
            //周用户注册
            var registeredData = repository.FindAsIQueryable(m => m.CreateDate.Value.Date > DateTime.Now.AddDays(-7))
                                .GroupBy(m => m.CreateDate.Value.Date).Select(m => new { date = m.Key, count = m.Count() }).ToList();
            var logindData = repository.FindAsIQueryable(m => m.LastLoginDate.Date > DateTime.Now.AddDays(-7))
                                .GroupBy(m => m.LastLoginDate.Date).Select(m => new { date = m.Key, count = m.Count() }).ToList();
            var contentData = repository.DbContext.Set<Content>().Where(m => m.CreateDate.Date > DateTime.Now.AddDays(-7) && m.IsPublic)
                                .GroupBy(m => m.CreateDate.Date).Select(m => new { date = m.Key, count = m.Count() }).ToList();
            var friendContentData = repository.DbContext.Set<Content>().Where(m => m.CreateDate.Date > DateTime.Now.AddDays(-7) && m.IsFriend)
                                .GroupBy(m => m.CreateDate.Date).Select(m => new { date = m.Key, count = m.Count() }).ToList();
            IList<DateTime> date = new List<DateTime>();
            for (int i = 1; i < 7; i++)
            {
                var currentDate = DateTime.Now.AddDays(-i);
                if (!registeredData.Exists(m => m.date == currentDate))
                    registeredData.Add(new { date = currentDate, count = 0 });
                if (!logindData.Exists(m => m.date == currentDate))
                    logindData.Add(new { date = currentDate, count = 0 });
                if (!contentData.Exists(m => m.date == currentDate))
                    contentData.Add(new { date = currentDate, count = 0 });
                if (!friendContentData.Exists(m => m.date == currentDate))
                    friendContentData.Add(new { date = currentDate, count = 0 });
                date.Add(currentDate);
            }
            return new
            {
                date = date.OrderBy(m => m.Date),
                data = new Dictionary<string, Array>()
                {
                    {"用户注册",registeredData.OrderBy(m=>m.date).Select(m=>m.count).ToArray() },
                    {"用户登录",logindData.OrderBy(m=>m.date).Select(m=>m.count).ToArray() },
                    {"发布内容",contentData.OrderBy(m=>m.date).Select(m=>m.count).ToArray() },
                    {"发布朋友圈",friendContentData.OrderBy(m=>m.date).Select(m=>m.count).ToArray() }
                }
            };
        }

        public object GetStatistic()
        {
            return new
            {
                user = repository.FindAsIQueryable(m => m.Enable == Entity.AppEnum.UserStatus.Enable).Count(),
                content = repository.DbContext.Set<Content>().Count(m => m.IsPublic),
                friendContent = repository.DbContext.Set<Content>().Count(m => m.IsPublic),
                intro = repository.DbContext.Set<Content_Intro>().Count(m => 1 == 1),
                comment = repository.DbContext.Set<Content_Comment>().Count(m => m.IsPublic)
            };
        }
        public object GetUserTop5()
        {
            var tops = repository.DbContext.Set<Content>().GroupBy(m => m.CreateID).Select(m => new { UserId = m.Key, Count = m.Count() }).Take(5);
            return repository.FindAsIQueryable(m => m.Enable == Entity.AppEnum.UserStatus.Enable && tops.Any(uu => uu.UserId == m.User_Id)).Select(m => new
            {
                m.Avatar,
                m.User_Id,
                m.NickName,
                m.Remark,
                m.LastLoginDate
            });
        }
    }
}
