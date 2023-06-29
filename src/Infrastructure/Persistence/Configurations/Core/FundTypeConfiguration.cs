using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Domain.Entities.Core;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class FundTypeConfiguration : IEntityTypeConfiguration<FundType>
    {
        public void Configure(EntityTypeBuilder<FundType> builder)
        {
            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.Name).HasMaxLength(30);
        }
    }
}
