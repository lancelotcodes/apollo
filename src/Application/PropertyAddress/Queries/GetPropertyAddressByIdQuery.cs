using apollo.Application.Common.Exceptions;
using apollo.Application.PropertyAddress.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.PropertyAddress.Queries
{
    public class GetPropertyAddressByIdQuery : IRequest<AddressDTO>
    {
        public int Id { get; set; }
    }

    public class GetPropertyAddressHandler : IRequestHandler<GetPropertyAddressByIdQuery, AddressDTO>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GetPropertyAddressHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AddressDTO> Handle(GetPropertyAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.UntrackFirstAsync<Property>(x => x.ID == request.Id && !x.IsDeleted,
                               i => i.Include(i => i.PropertyType)
                               .Include(i => i.Address).ThenInclude(i => i.City)
                               .Include(i => i.Address).ThenInclude(i => i.MicroDistrict)
                               .Include(i => i.Address).ThenInclude(i => i.SubMarket));

            if (property.Address == null)
                throw new NotFoundException();

            return _mapper.Map<AddressDTO>(property);
        }
    }

}
