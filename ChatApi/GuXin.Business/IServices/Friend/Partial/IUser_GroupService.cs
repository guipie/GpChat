/*
*所有关于User_Group_Setting类的业务代码接口应在此处编写
*/
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using System.Linq;
using GuXin.Entity.AppDto.Output;
using System.Collections.Generic;

namespace GuXin.Business.IServices
{
    public partial interface IUser_GroupService
    {
        /// <summary>
        /// 新增群
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        WebResponseContent Post(int[] userIds);

        /// <summary>
        /// 添加群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        WebResponseNormal Put(int groupId, int[] userId);
        /// <summary>
        /// 删除群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        WebResponseContent Delete(int groupId, int[] userIds);
        WebResponseContent Quit(int groupId);
        int[] GetAllGroups();
        IList<GroupInfo> GetAllGroupsInfo();
        IQueryable<object> GetMySaveGroups();
        GroupInfo GetGroupInfo(int groupId);
        bool GroupSettings(int groupId, bool? notDisturb, bool? top, bool? saveMy, bool? accept);
        bool GroupNameDiscription(int groupId, string name, string discription);
        WebResponseContent SetNickName(int groupId, string nickName);
    }
}
