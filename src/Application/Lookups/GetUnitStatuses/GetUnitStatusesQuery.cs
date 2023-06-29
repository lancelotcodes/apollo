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

namespace apollo.Application.Lookups.UnitStatuses
{
    public class GetUnitStatusesQuery : IRequest<IEnumerable<UnitStatusDTO>>
    {
    }

    public class GetUnitStatusesQueryHandler : IRequestHandler<GetUnitStatusesQuery, IEnumerable<UnitStatusDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetUnitStatusesQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<UnitStatusDTO>> Handle(GetUnitStatusesQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.Fetch<UnitStatus>();

            return await data.ProjectTo<UnitStatusDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
