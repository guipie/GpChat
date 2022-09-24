using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Content_XinPraiseMapConfig : EntityMappingConfiguration<Content_XinPraise>
    {
        public override void Map(EntityTypeBuilder<Content_XinPraise>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

