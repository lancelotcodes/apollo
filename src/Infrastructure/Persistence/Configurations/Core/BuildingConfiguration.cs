using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(i => i.PropertyID);
            builder.HasOne(i => i.Property);

            builder.HasIndex(i => i.LeasingContactID);

            builder.HasIndex(i => i.DeveloperID);

            builder.HasIndex(i => i.PropertyManagementID);

            builder.HasMany(i => i.Floors);

            builder.HasIndex(i => i.OwnershipTypeID);
            builder.HasOne(i => i.OwnershipType);

            builder.HasIndex(i => i.ProjectStatusID);
            builder.HasOne(i => i.ProjectStatus);
        }
    }
}
