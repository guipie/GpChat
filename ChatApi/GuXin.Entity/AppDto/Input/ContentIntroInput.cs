using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuXin.Entity.DomainModels;

namespace GuXin.Entity.AppDto.Input
{
    public class ContentIntroInput
    {
        public Guid? IntroGuid { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }
        public string Introduction { get; set; }
        public List<Content_File> Pics { get; set; }

        /// <summary>
        /// 修改时删除的图片ID
        /// </summary>
        public Guid[]? DelFiles { get; set; }
    }
}
