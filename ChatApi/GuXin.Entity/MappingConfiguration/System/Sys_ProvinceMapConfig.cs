using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Sys_ProvinceMapConfig : EntityMappingConfiguration<Sys_Province>
    {
        public override void Map(EntityTypeBuilder<Sys_Province>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

