using apollo.Domain.Entities.References;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.References.Cities
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(i => i.Name).HasMaxLength(50);
            builder.HasIndex(i => i.ProvinceID);

            builder.HasMany(i => i.SubMarkets);
            builder.HasMany(i => i.MicroDistricts);

        }
    }
}
