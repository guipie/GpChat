using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class User_Tag_MemberMapConfig : EntityMappingConfiguration<User_Tag_Member>
    {
        public override void Map(EntityTypeBuilder<User_Tag_Member>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

