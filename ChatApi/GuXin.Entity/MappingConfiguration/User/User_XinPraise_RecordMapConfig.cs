using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class User_XinPraise_RecordMapConfig : EntityMappingConfiguration<User_XinPraise_Record>
    {
        public override void Map(EntityTypeBuilder<User_XinPraise_Record>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

