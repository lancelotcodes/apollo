using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.Shared;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.PropertyAddress.Commands.SavePropertyAddress
{
    public class SavePropertyAddressHandler : IRequestHandler<SavePropertyAddressRequest, SaveEntityResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public SavePropertyAddressHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveEntityResult> Handle(SavePropertyAddressRequest request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Address>(request);
            if (address == null) throw new BadRequestException("Unable to create Address.");

            address.IsActive = true;
            _repository.Save(address);
            await _repository.SaveChangesAsync();

            if (request.ID == 0)
            {
                var property = _repository.GetById<Property>(request.PropertyID);
                property.AddressID = address.ID;
                await _repository.SaveChangesAsync();
            }

            return new SaveEntityResult { EntityId = address.ID };
        }
    }
}
