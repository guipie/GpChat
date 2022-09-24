using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    public class AppContent
    {
        public Guid guid { get; set; }
        public Guid? parentGuid { get; set; }
        public object parent { get; set; }
        public string contents { get; set; }

        public DateTime createDate { get; set; }

        public int collectCount { get; set; }
        public int shareCount { get; set; }
        public int xinPraiseCount { get; set; }
        public int praiseCount { get; set; }
        public int commentCount { get; set; }
        public int createID { get; set; }
        public object pics { get; set; }
        public ContentUser contentUser { get; set; }
        public bool isFollowing { get; set; }


        public bool isXinPraised { get; set; }
        public bool isPraised { get; set; }
        public bool isShared { get; set; }
        public bool isCollected { get; set; }
        public bool isCommented { get; set; }

        public AppContent() { }
    }

    public class Pic
    {
        public string path { set; get; }
        public float length { set; get; }
    }
    public class ContentUser
    {
        public string nickName { get; set; }
        public string avatar { get; set; }
        public int userId { get; set; }
        public string remark { get; set; }
    }
}
