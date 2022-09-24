using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class App_UpgradeMapConfig : EntityMappingConfiguration<App_Upgrade>
    {
        public override void Map(EntityTypeBuilder<App_Upgrade>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

