using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasMany(i => i.PropertyOwned).WithOne(a => a.Owner).HasForeignKey(x => x.OwnerID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(i => i.PropertyListed).WithOne(a => a.Contact).HasForeignKey(x => x.ContactID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
