using apollo.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apollo.Infrastructure.Persistence.Configurations.Core
{
    public class ContractsConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasIndex(i => i.CompanyID);
            builder.HasOne(i => i.Company).WithMany(a => a.Contracts).OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(i => i.ContactID);
            builder.HasOne(i => i.Contact).WithMany(a => a.Contracts).OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(i => i.BrokerCompanyID);
            builder.HasOne(i => i.BrokerCompany).WithMany(a => a.BrokerContracts).OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(i => i.BrokerID);
            builder.HasOne(i => i.Broker).WithMany(a => a.BrokerContracts).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
