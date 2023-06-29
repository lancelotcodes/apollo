using apollo.Application.Common.Exceptions;
using apollo.Application.Floors.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Floors.Queries.GetFloorsByBuildingID
{
    public class GetFloorByIDQuery : IRequest<FloorDTO>
    {
        public int Id { get; set; }
    }

    public class GetFloorByIDHandler : IRequestHandler<GetFloorByIDQuery, FloorDTO>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetFloorByIDHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FloorDTO> Handle(GetFloorByIDQuery request, CancellationToken cancellationToken)
        {
            var floor = await _repository.UntrackFirstAsync<Floor>(x => x.ID == request.Id && !x.IsDeleted,
                             i => i.Include(i => i.Units).ThenInclude(x => x.Property).ThenInclude(x => x.Agents).ThenInclude(x => x.Agent)
                                   .Include(x => x.Units).ThenInclude(x => x.Property)
                                        .ThenInclude(x => x.Contracts).ThenInclude(x => x.TenantClassification)
                                   .Include(x => x.Units).ThenInclude(x => x.ListingType)
                                   .Include(x => x.Units).ThenInclude(x => x.UnitStatus)
                                   .Include(x => x.Units).ThenInclude(x => x.HandOverCondition));
            if (floor == null)
                throw new NotFoundException();

            return _mapper.Map<FloorDTO>(floor);
        }
    }
}