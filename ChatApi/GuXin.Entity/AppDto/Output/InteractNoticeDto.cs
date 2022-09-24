using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Output
{
    public class InteractNoticeDto
    {
        public string Avatar { get; set; }
        public string NickName { get; set; }
        public string Type { get; set; }
        public Guid ContentGuid { get; set; }
        public string Contents { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateID { get; set; }
        public string[] Pics { get; set; }
        public int PicCount { get; set; }
        public int PraiseCount { get; set; }
    }
}
