using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class User_Group_MemberMapConfig : EntityMappingConfiguration<User_Group_Member>
    {
        public override void Map(EntityTypeBuilder<User_Group_Member>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

