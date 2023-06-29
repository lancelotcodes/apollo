using apollo.Domain.Entities.References;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Persistence.Configurations.References.Regions
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.Property(i => i.Name).HasMaxLength(50);
            builder.HasIndex(i => i.CountryID);
            builder.HasMany(i => i.Provinces);
        }
    }
}
