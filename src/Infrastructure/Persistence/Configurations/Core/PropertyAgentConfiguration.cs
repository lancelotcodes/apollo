using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{

    public class PropertyAgentConfiguration : IEntityTypeConfiguration<PropertyAgent>
    {
        public void Configure(EntityTypeBuilder<PropertyAgent> builder)
        {
            builder.HasIndex(i => i.PropertyID);
            builder.HasOne(i => i.Agent).WithMany().HasForeignKey(x => x.AgentID).IsRequired();
        }
    }
}
