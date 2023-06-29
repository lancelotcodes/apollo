using apollo.Application.Lookups.DTOs;
using apollo.Domain.Entities.References;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Lookups.ListingTypes
{
    public class GetListingTypesQuery : IRequest<IEnumerable<ListingTypeDTO>>
    {
    }

    public class GetListingTypesQueryHandler : IRequestHandler<GetListingTypesQuery, IEnumerable<ListingTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetListingTypesQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<ListingTypeDTO>> Handle(GetListingTypesQuery request, CancellationToken cancellationToken)
        {
            var availabilities = _repository.Fetch<ListingType>();

            return await availabilities.ProjectTo<ListingTypeDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
