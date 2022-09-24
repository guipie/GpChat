using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class User_XinPraiseMapConfig : EntityMappingConfiguration<User_XinPraise>
    {
        public override void Map(EntityTypeBuilder<User_XinPraise>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

