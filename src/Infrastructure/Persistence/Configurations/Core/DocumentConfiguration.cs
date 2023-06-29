using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasIndex(x => x.DocumentKey).IsUnique();
            builder.Property(i => i.DocumentName)
              .IsRequired()
              .HasMaxLength(100);
            builder.Property(i => i.DocumentPath)
             .IsRequired();

            builder.HasOne(x => x.ThumbNail).WithMany().HasForeignKey(x => x.ThumbNailId);
        }
    }
}
