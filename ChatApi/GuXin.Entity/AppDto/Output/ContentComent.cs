using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    public class ContentComent
    {
        public int id { get; set; }
        public int? parentId { get; set; }
        public CommentUser commentUser { get; set; }
        public CommentUser parentcommentUser { get; set; }
        public string comments { get; set; }
        public int praiseCount { get; set; }
        public bool praised { get; set; }
        public int createId { get; set; }
        public DateTime createDate { get; set; }

    }
    public class CommentUser
    {
        public string nickName { get; set; }
        public string avatar { get; set; }
        public int userId { get; set; }
    }
}
