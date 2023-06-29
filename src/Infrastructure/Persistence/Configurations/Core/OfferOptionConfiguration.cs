using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class OfferOptionConfiguration : IEntityTypeConfiguration<OfferOption>
    {
        public void Configure(EntityTypeBuilder<OfferOption> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(i => i.User).WithMany().HasForeignKey(x => x.UserID).IsRequired();
            builder.HasOne(i => i.ListingType).WithMany().HasForeignKey(x => x.ListingTypeID).IsRequired(false);
            builder.HasOne(i => i.Company).WithMany().HasForeignKey(x => x.CompanyID).IsRequired(false);
            builder.HasOne(i => i.Contact).WithMany().HasForeignKey(x => x.ContactID).IsRequired(false);
            builder.HasOne(i => i.Agent).WithMany().HasForeignKey(x => x.AgentID).IsRequired(true);
            builder.HasOne(i => i.HandOverCondition).WithMany().HasForeignKey(x => x.HandOverConditionID).IsRequired(false);
            builder.HasOne(i => i.PropertyType).WithMany().HasForeignKey(x => x.PropertyTypeID).IsRequired(false);
        }
    }

    public class OfferUnitConfiguration : IEntityTypeConfiguration<OfferUnit>
    {
        public void Configure(EntityTypeBuilder<OfferUnit> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(i => i.Unit).WithMany().HasForeignKey(x => x.UnitId).IsRequired();
            builder.HasOne(i => i.OfferOption).WithMany(x => x.OfferUnits).HasForeignKey(x => x.OfferId).IsRequired();
        }
    }
}
