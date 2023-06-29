using apollo.Domain.Entities.References;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.References.Countries
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(i => i.Name).HasMaxLength(50);

            builder.HasMany(i => i.States);
            builder.HasMany(i => i.Regions);

        }
    }
}
