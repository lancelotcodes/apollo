using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Application.Units.Queries.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using Shared.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Unit = apollo.Domain.Entities.Core.Unit;

namespace apollo.Application.Floors.Queries.GetFloorsByBuildingID
{

    public class GetUnitsByBuildingIDQuery : ListQueryBase, IRequest<PaginatedList<UnitShortDetailDTO>>
    {
        [FromRoute]
        public int Id { get; set; }

        public string Search { get; set; }

        public string Status { get; set; }

        public List<string> ExpiryYears { get; set; }
    }

    public class GetUnitsByBuildingIDHandler : IRequestHandler<GetUnitsByBuildingIDQuery, PaginatedList<UnitShortDetailDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetUnitsByBuildingIDHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UnitShortDetailDTO>> Handle(GetUnitsByBuildingIDQuery request, CancellationToken cancellationToken)
        {

            var query = _repository.Fetch<Unit>(x => x.Floor.BuildingID == request.Id && !x.IsDeleted,
                            i =>
                            i.Include(x => x.Property).ThenInclude(x => x.Contracts).ThenInclude(x => x.Company)
                            .Include(x => x.Property).ThenInclude(x => x.Contracts).ThenInclude(x => x.TenantClassification)
                            .Include(x => x.UnitStatus)
                            .Include(x => x.Floor)
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

            var data = await query.ProjectTo<UnitShortDetailDTO>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);

            return data
                .OrderByDescending(x => x.FloorSort)
               .AsQueryable()
               .ToPaginatedList(request.PageNumber, request.PageSize);
        }
    }
}