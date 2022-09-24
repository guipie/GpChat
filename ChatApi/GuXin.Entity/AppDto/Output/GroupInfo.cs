using GuXin.Entity.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    public class GroupInfo : User_Group_Setting
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IQueryable<GroupMember> Members { get; set; }

        public GroupInfo() { }
        public GroupInfo(User_Group_Setting setting, int groupId, string name, string desc, int createId, DateTime created, DateTime? mofied)
        {
            this.GroupId = groupId;
            this.Name = name;
            this.Description = desc;
            this.Id = setting.Id;
            this.NotDisturb = setting.NotDisturb;
            this.Top = setting.Top;
            this.SaveGroup = setting.SaveGroup;
            this.AcceptAdd = setting.AcceptAdd;
            this.CreateDate = created;
            this.ModifyDate = mofied;
            this.CreateID = createId;
        }
    }
    public class GroupMember
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
