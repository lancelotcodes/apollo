using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Commands.DeleteProperty
{
    public class DeletePropertyHandler : IRequestHandler<DeletePropertyRequest, bool>
    {
        private readonly IRepository _repository;

        public DeletePropertyHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
        {
            var property = _repository.Get<Property>(x => x.ID == request.PropertyID);

            if (property == null || property.IsDeleted)
            {
                throw new NotFoundException("Property not found.");
            }
            _repository.Delete(property);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
