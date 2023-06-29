using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyDocument>
    {
        public void Configure(EntityTypeBuilder<PropertyDocument> builder)
        {
            builder.Property(i => i.DocumentID)
                .IsRequired();
            builder.HasOne(i => i.Document).WithOne().HasForeignKey<PropertyDocument>(x => x.DocumentID).HasPrincipalKey<Document>(x => x.ID);
        }
    }
}
