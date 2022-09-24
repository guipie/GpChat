using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class User_Sys_Notice_SettingMapConfig : EntityMappingConfiguration<User_Sys_Notice_Setting>
    {
        public override void Map(EntityTypeBuilder<User_Sys_Notice_Setting>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

