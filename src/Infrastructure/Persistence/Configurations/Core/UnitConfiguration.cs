using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasOne(i => i.Floor).WithMany(a => a.Units).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.Property).WithOne().HasForeignKey<Unit>(x => x.PropertyID).HasPrincipalKey<Property>(x => x.ID);

            builder.HasOne(i => i.HandOverCondition).WithMany().HasForeignKey(x => x.HandOverConditionID);

        }
    }
}
