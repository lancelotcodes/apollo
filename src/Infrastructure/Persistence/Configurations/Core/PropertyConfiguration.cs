using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(i => i.PropertyTypeID);
            builder.HasOne(i => i.PropertyType);
            builder.HasOne(i => i.Building).WithOne(x => x.Property).HasForeignKey<Building>(x => x.PropertyID);

            builder.HasIndex(i => i.SubTypeID);
            builder.HasOne(i => i.SubType);

            builder.HasIndex(i => i.GradeID);
            builder.HasOne(i => i.Grade);

            builder.HasIndex(i => i.AddressID);
            builder.HasOne(i => i.Address);

            builder.HasIndex(i => i.SEOID);
            builder.HasOne(i => i.SEO);

            builder.HasIndex(i => i.ContactID);

            builder.HasIndex(i => i.OwnerID);

            builder.HasMany(i => i.Agents);

            builder.HasMany(i => i.Contracts);

            builder.HasMany(i => i.PropertyDocuments);

            builder.HasIndex(i => i.MasterPropertyID);

            builder.HasIndex(i => i.ResidentialUnitID);
            builder.HasIndex(i => i.ListingID);
            builder.HasOne(i => i.ResidentialUnit).WithOne(i => i.Listing).HasForeignKey<Property>(x => x.ResidentialUnitID).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(i => i.Buildings).WithOne(x => x.MasterProperty).HasForeignKey(x => x.MasterPropertyID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
