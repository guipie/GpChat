using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Content_CollectMapConfig : EntityMappingConfiguration<Content_Collect>
    {
        public override void Map(EntityTypeBuilder<Content_Collect>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

