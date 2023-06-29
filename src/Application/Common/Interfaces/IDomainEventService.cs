using Shared.Domain.Common;
using System.Threading.Tasks;

namespace apollo.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
