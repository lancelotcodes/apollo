using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Common.Extensions;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Queries.GetBuildingList
{

    public class GetBuildingListQuery : ListQueryBase, IRequest<PaginatedList<SimpleBuildingDTO>>
    {
        public string Filter { get; set; }

        public string Coordinates { get; set; }
    }

    public class GetAllBuildingsHandler : IRequestHandler<GetBuildingListQuery, PaginatedList<SimpleBuildingDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllBuildingsHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<SimpleBuildingDTO>> Handle(GetBuildingListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Building> buildings = _context.Buildings
                .Include(i => i.Property)
                    .ThenInclude(i => i.SEO)
                .Include(i => i.Property)
                    .ThenInclude(i => i.PropertyType)
                .Include(i => i.Property)
                    .ThenInclude(i => i.Grade)
                .Include(i => i.Property)
                    .ThenInclude(i => i.Address)
                        .ThenInclude(i => i.City)
                            .ThenInclude(i => i.Province)
                                .ThenInclude(i => i.Region)
                                    .ThenInclude(i => i.Country);

            buildings = buildings.Where(b => (string.IsNullOrEmpty(request.Filter) || b.Name.ToLower().Contains(request.Filter) ||
            b.Property.Address.Line1.ToLower().Contains(request.Filter) ||
            b.Property.Address.Line2.ToLower().Contains(request.Filter) ||
            b.Property.Address.MicroDistrict.Name.ToLower().Contains(request.Filter) ||
            b.Property.Address.SubMarket.Name.ToLower().Contains(request.Filter) ||
            b.Property.Address.City.Name.ToLower().Contains(request.Filter) ||
            b.Property.Address.City.Province.Name.ToLower().Contains(request.Filter) ||
            b.Property.Address.City.Province.Region.Name.ToLower().Contains(request.Filter) ||
            b.Property.Address.City.Province.Region.Country.Name.ToLower().Contains(request.Filter) ||
            b.Property.PropertyType.Name.ToLower().Contains(request.Filter))
            && b.IsDeleted == false);

            var list = await buildings.ProjectTo<SimpleBuildingDTO>(_mapper.ConfigurationProvider).ToListAsync();

            if (!string.IsNullOrEmpty(request.Coordinates))
            {
                list.ForEach(x => x.Distance = Helpers.GetDistance(request.Coordinates, x.Latitude.ToString() + "," + x.Longitude.ToString()));
                list = list.OrderBy(x => x.Distance).ToList();
            }

            return list
                .AsQueryable()
                .ToPaginatedList(request.PageNumber, request.PageSize);
        }
    }
}
