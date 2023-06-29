using apollo.Domain.Entities.References;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Persistence.Configurations.References.Grades
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.Property(i => i.Name).HasMaxLength(50);
            builder.HasIndex(i => i.PropertyTypeID);
            builder.HasOne(i => i.PropertyType);
        }
    }
}
