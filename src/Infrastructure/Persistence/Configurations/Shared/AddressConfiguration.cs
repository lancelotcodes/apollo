using apollo.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Shared
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasIndex(i => i.CityID);
            builder.HasOne(i => i.City);
            builder.HasIndex(i => i.SubMarketID);
            builder.HasOne(i => i.SubMarket);
            builder.HasIndex(i => i.MicroDistrictID);
            builder.HasOne(i => i.MicroDistrict);
        }
    }
}
