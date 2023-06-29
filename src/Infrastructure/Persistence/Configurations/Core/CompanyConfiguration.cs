using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(i => i.LeasedBuildings).WithOne(a => a.LeasingContact).HasForeignKey(x => x.LeasingContactID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(i => i.DevelopedBuildings).WithOne(a => a.Developer).HasForeignKey(x => x.DeveloperID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(i => i.PropertyManagedBuildings).WithOne(a => a.PropertyManagement).HasForeignKey(x => x.PropertyManagementID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
