using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuXin.Entity.AppDto.Input
{
    public class TagDto
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
        public int[] memberUserIds { get; set; }
    }
}
