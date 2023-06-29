using apollo.Application.Floors.Queries.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Units.Queries.GetUnitByID
{
    public class GetStackingPlanDataFroExportQuery : IRequest<IEnumerable<ExportStackingPlanDTO>>
    {
        public int Id { get; set; }
    }

    public class GetStackingPlanDataFroExportQueryHandler : IRequestHandler<GetStackingPlanDataFroExportQuery, IEnumerable<ExportStackingPlanDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetStackingPlanDataFroExportQueryHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExportStackingPlanDTO>> Handle(GetStackingPlanDataFroExportQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Fetch<Domain.Entities.Core.Unit>(x => x.Floor.BuildingID == request.Id && !x.IsDeleted,
                i => i.Include(x => x.Availability)
                .Include(x => x.ListingType)
                .Include(x => x.HandOverCondition)
                .Include(x => x.UnitStatus)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.Company)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.Contact)
                .Include(x => x.Property.Contracts).ThenInclude(x => x.TenantClassification)
                .Include(i => i.Floor).ThenInclude(x => x.Building)).ProjectTo<ExportStackingPlanDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}