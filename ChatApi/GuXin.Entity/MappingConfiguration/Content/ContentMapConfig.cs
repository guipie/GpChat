using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class ContentMapConfig : EntityMappingConfiguration<Content>
    {
        public override void Map(EntityTypeBuilder<Content>
        builderTable)
        {
            //b.Property(x => x.StorageName).HasMaxLength(45);  
            //builderTable.HasOne(m => m.UserInfo).WithMany(m => m.Contents).HasForeignKey(m => m.CreateID);
        }
    }
}

