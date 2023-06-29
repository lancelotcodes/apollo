using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class AppraisalConfiguration : IEntityTypeConfiguration<Appraisal>
    {
        public void Configure(EntityTypeBuilder<Appraisal> builder)
        {
            builder.HasIndex(i => i.ValuationTypeID);
            builder.HasOne(i => i.ValuationType);

            builder.HasIndex(i => i.ValuationApproachID);
            builder.HasOne(i => i.ValuationApproach);

        }
    }
}
