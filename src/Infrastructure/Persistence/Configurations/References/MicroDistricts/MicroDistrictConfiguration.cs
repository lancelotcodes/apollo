using apollo.Domain.Entities.References;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Persistence.Configurations.References.MicroDistricts
{
    public class MicroDistrictConfiguration : IEntityTypeConfiguration<MicroDistrict>
    {
        public void Configure(EntityTypeBuilder<MicroDistrict> builder)
        {
            builder.Property(i => i.Name).HasMaxLength(50);
            builder.HasIndex(i => i.CityID);
            builder.HasOne(i => i.City);
        }
    }
}
