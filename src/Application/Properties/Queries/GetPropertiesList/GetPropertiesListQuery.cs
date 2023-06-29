using apollo.Application.Common.Extensions;
using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Application.Properties.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using Shared.Pagination;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Queries.GetProperties
{
    public class GetPropertiesListQuery : ListQueryBase, IRequest<PaginatedList<PropertyListDTO>>
    {
        public string Query { get; set; }
        public string City { get; set; }
        public string SubMarket { get; set; }
        public string MicroDistrict { get; set; }
        public string Project { get; set; }
        public string Grade { get; set; }
        public string PropertyType { get; set; }
        public string Coordinates { get; set; }
    }

    public class GetPropertiesListHandler : IRequestHandler<GetPropertiesListQuery, PaginatedList<PropertyListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetPropertiesListHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<PaginatedList<PropertyListDTO>> Handle(GetPropertiesListQuery request, CancellationToken cancellationToken)
        {
            var properties = _repository.Fetch<Property>(b => (string.IsNullOrWhiteSpace(request.Query) ||
                               (b.Address.MicroDistrict.Name.Contains(request.Query) ||
                                b.Address.SubMarket.Name.Contains(request.Query) ||
                                b.Address.City.Name.Contains(request.Query) ||
                                b.Address.City.Province.Name.Contains(request.Query) ||
                                b.Address.Line1.Contains(request.Query) ||
                                b.Name.Contains(request.Query) ||
                                b.PropertyType.Name.Contains(request.Query) ||
                                b.Grade.Name.Contains(request.Query) ||
                                b.Building.Developer.Name.Contains(request.Query) ||
                                b.Building.LeasingContact.Name.Contains(request.Query) ||
                                b.Address.City.Province.Region.Name.Contains(request.Query) ||
                                b.Address.City.Province.Region.Country.Name.Contains(request.Query)))
                                && (string.IsNullOrWhiteSpace(request.City) || b.Address.City.Name.Equals(request.City))
                                && (string.IsNullOrWhiteSpace(request.SubMarket) || b.Address.SubMarket.Name.Equals(request.SubMarket))
                                && (string.IsNullOrWhiteSpace(request.MicroDistrict) || b.Address.MicroDistrict.Name.Equals(request.MicroDistrict))
                                && (string.IsNullOrWhiteSpace(request.Project) || b.MasterProject.Name.Equals(request.Project))
                                && (string.IsNullOrWhiteSpace(request.PropertyType) || b.PropertyType.Name.Equals(request.PropertyType))
                                && (string.IsNullOrWhiteSpace(request.Grade) || b.PropertyType.Name.Equals(request.Grade))
                                && b.IsDeleted == false,
                                i => i.Include(i => i.PropertyType)
                               .Include(i => i.Address)
                               .Include(i => i.Agents)
                               .Include(i => i.Grade)
                               .Include(i => i.PropertyDocuments.Where(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage))
                               .ThenInclude(x => x.Document));

            var list = await properties.ProjectTo<PropertyListDTO>(_mapper.ConfigurationProvider).ToListAsync();

            if (!string.IsNullOrEmpty(request.Coordinates))
            {
                list.Where(x => x.AddressID != null).ToList().ForEach(x => x.Distance = Helpers.GetDistance(request.Coordinates, x.Latitude.ToString() + "," + x.Longitude.ToString()));
                list = list.OrderBy(x => x.Distance).ToList();
            }

            return list
                .AsQueryable()
                .ToPaginatedList(request.PageNumber, request.PageSize);
        }
    }
}
