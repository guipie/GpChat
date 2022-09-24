using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Sys_AgreementMapConfig : EntityMappingConfiguration<Sys_Agreement>
    {
        public override void Map(EntityTypeBuilder<Sys_Agreement>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

