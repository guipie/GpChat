using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Content_FileMapConfig : EntityMappingConfiguration<Content_File>
    {
        public override void Map(EntityTypeBuilder<Content_File>
        builderTable)
        {
            //b.Property(x => x.StorageName).HasMaxLength(45);

            //builderTable.HasOne(cf => cf.Content).WithMany(c => c.ContentFiles).HasForeignKey(m => m.ContentGuid);
        }
    }
}

