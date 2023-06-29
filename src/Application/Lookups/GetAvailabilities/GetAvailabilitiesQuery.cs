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

namespace apollo.Application.Lookups.Availabilities
{
    public class GetAvailabilitiesQuery : IRequest<IEnumerable<AvailabilityDTO>>
    {
    }

    public class GetAvailabilitesQueryHandler : IRequestHandler<GetAvailabilitiesQuery, IEnumerable<AvailabilityDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetAvailabilitesQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<AvailabilityDTO>> Handle(GetAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            var availabilities = _repository.Fetch<Availability>();

            return await availabilities.ProjectTo<AvailabilityDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
