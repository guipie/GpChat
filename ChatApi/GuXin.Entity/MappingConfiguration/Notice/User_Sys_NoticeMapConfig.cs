using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class User_Sys_NoticeMapConfig : EntityMappingConfiguration<User_Sys_Notice>
    {
        public override void Map(EntityTypeBuilder<User_Sys_Notice>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

