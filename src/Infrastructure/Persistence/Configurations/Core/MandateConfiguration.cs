using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class MandateConfiguration : IEntityTypeConfiguration<Mandate>
    {
        public void Configure(EntityTypeBuilder<Mandate> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.HasOne(x => x.Property).WithMany(x => x.Mandates).HasForeignKey(x => x.PropertyID);
            builder.HasOne(x => x.Attachment).WithMany().HasForeignKey(x => x.AttachmentId);
        }
    }
}
