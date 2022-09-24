using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Content_Comment_PraiseMapConfig : EntityMappingConfiguration<Content_Comment_Praise>
    {
        public override void Map(EntityTypeBuilder<Content_Comment_Praise>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

