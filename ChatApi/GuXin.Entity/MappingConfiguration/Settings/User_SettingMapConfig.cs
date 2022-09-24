using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class User_SettingMapConfig : EntityMappingConfiguration<User_Setting>
    {
        public override void Map(EntityTypeBuilder<User_Setting>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

