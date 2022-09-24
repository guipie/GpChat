using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Sys_SettingMapConfig : EntityMappingConfiguration<Sys_Setting>
    {
        public override void Map(EntityTypeBuilder<Sys_Setting>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

