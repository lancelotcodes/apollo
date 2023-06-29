using apollo.Application.Floors.Queries.DTOs;
using apollo.Application.Units.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Unit = apollo.Domain.Entities.Core.Unit;

namespace apollo.Application.Floors.Queries.GetFloorsByBuildingID
{
    public class GetFloorsByBuildingIDQuery : IRequest<List<FloorShortDetailDTO>>
    {
        [FromRoute]
        public int Id { get; set; }

        public string Search { get; set; }

        public string Status { get; set; }

        public List<string> ExpiryYears { get; set; }
    }

    public class GetFloorsByBuildingIDHandler : IRequestHandler<GetFloorsByBuildingIDQuery, List<FloorShortDetailDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetFloorsByBuildingIDHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FloorShortDetailDTO>> Handle(GetFloorsByBuildingIDQuery request, CancellationToken cancellationToken)
        {

            var data = await _repository.Fetch<Floor>(x => (string.IsNullOrWhiteSpace(request.Search) ||
                            x.Name.Contains(request.Search))
                            && x.BuildingID == request.Id && !x.IsDeleted)
                .ToListAsync(cancellationToken);

            var mappedFloors = _mapper.Map<List<FloorShortDetailDTO>>(data);

            if (mappedFloors != null)
            {
                mappedFloors = mappedFloors.OrderByDescending(x => x.Sort).ToList();
            }

            var units = await GetFilteredUnits(request);

            foreach (var item in mappedFloors)
            {
                item.Units = units.Where(x => x.FloorID == item.ID).ToList();
            }

            return mappedFloors;
        }

        private async Task<IEnumerable<UnitShortDetailDTO>> GetFilteredUnits(GetFloorsByBuildingIDQuery request)
        {

            var query = _repository.Fetch<Unit>(x => x.Floor.BuildingID == request.Id && !x.IsDeleted,
                            i => i.Include(x => x.Property).ThenInclude(x => x.Contracts).ThenInclude(x => x.Company)
                            .Include(x => x.Property).ThenInclude(x => x.Contracts).ThenInclude(x => x.TenantClassification)
                            .Include(x => x.UnitStatus)
                            );


            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search) || x.UnitNumber.Contains(request.Search));
            }

            if (!string.IsNullOrWhiteSpace(request.Status) && request.Status != "All")
            {
                query = query.Where(x => x.UnitStatus.Name == request.Status);
            }

            if (request.ExpiryYears != null && request.ExpiryYears.Any())
            {
                var years = request.ExpiryYears.Select(x => Regex.Match(x, @"\d+").Value);
                query = query.Where(x => x.Property.Contracts.Count(x => !x.IsHistorical && x.EndDate != null && years.Contains(x.EndDate.Value.Year.ToString())) > 0);
            }

            return await query.ProjectTo<UnitShortDetailDTO>(_mapper.ConfigurationProvider)
               .ToListAsync();
        }
    }

}