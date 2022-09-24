using GuXin.Entity.MappingConfiguration;
using GuXin.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuXin.Entity.MappingConfiguration
{
    public class Sys_UserMapConfig : EntityMappingConfiguration<Sys_User>
    {
        public override void Map(EntityTypeBuilder<Sys_User>
        builderTable)
        {
            //b.Property(x => x.StorageName).HasMaxLength(45);

            //builderTable.HasMany(cf => cf.Contents).WithOne(c => c.UserInfo).HasForeignKey(m => m.CreateID);
        }
    }
}

