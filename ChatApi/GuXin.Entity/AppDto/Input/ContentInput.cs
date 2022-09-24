using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuXin.Entity.DomainModels;

namespace GuXin.Entity.AppDto.Input
{
    public class ContentInput
    {
        public Guid Guid { get; set; }
        public Guid? ParentGuid { get; set; }
        public string Content { get; set; }
        public bool IsFriend { get; set; }
        public bool IsPublic { get; set; }
        public int? VisibleDays { get; set; }
        public List<ContentFileInput> Files { get; set; }
        public Guid[] DelFiles { get; set; }
    }
    public class ContentFileInput
    {
        public Guid? Guid { get; set; }
        public Guid? ContentGuid { get; set; }
        public string ContentType { get; set; }
        public string Orientation { get; set; }
        public string Name { get; set; }
        public string UploadPath { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }
        public bool? IsFriend { get; set; }
        public bool? IsPublic { get; set; }
    }
}
