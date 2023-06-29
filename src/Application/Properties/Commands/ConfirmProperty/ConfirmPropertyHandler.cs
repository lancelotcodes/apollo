using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using MediatR;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Commands.ConfirmProperty
{
    public class ConfirmPropertyHandler : IRequestHandler<ConfirmPropertyRequest, SaveEntityResult>
    {
        private readonly IRepository _repository;

        public ConfirmPropertyHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<SaveEntityResult> Handle(ConfirmPropertyRequest request, CancellationToken cancellationToken)
        {
            var property = _repository.Get<Property>(x => x.ID == request.PropertyID, y => y.Include(i => i.Agents));

            if (property == null || property.IsDeleted)
            {
                throw new NotFoundException("Property not found.");
            }
            property.IsActive = true;
            await _repository.SaveChangesAsync();
            return new SaveEntityResult { EntityId = property.ID };
        }
    }
}
