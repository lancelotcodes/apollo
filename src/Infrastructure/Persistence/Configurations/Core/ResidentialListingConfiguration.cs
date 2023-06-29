using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class ResidentialListingConfiguration : IEntityTypeConfiguration<ResidentialListing>
    {
        public void Configure(EntityTypeBuilder<ResidentialListing> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(i => i.PropertyID);
            builder.HasOne(i => i.Property);

            builder.HasIndex(i => i.ListingTypeID);
            builder.HasOne(i => i.ListingType);

            builder.HasIndex(i => i.AgentID);
            builder.HasOne(i => i.Agent).WithOne().HasForeignKey<ResidentialListing>(x => x.AgentID).HasPrincipalKey<Contact>(x => x.ID);

        }
    }
}
