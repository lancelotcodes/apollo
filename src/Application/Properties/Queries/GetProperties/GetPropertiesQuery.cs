using apollo.Application.Properties.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Queries.GetProperties
{
    public class GetPropertiesQuery : IRequest<IEnumerable<MapPropertyListDTO>>
    {
        public int? Type { get; set; }
        public string Location { get; set; }
    }

    public class GetPropertiesHandler : IRequestHandler<GetPropertiesQuery, IEnumerable<MapPropertyListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetPropertiesHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<MapPropertyListDTO>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = _repository.Fetch<Property>(b => (string.IsNullOrWhiteSpace(request.Location) ||
                               (b.Address.MicroDistrict.Name.ToLower().Contains(request.Location) ||
                                b.Address.SubMarket.Name.ToLower().Contains(request.Location) ||
                                b.Address.City.Name.ToLower().Contains(request.Location) ||
                                b.Address.City.Province.Name.ToLower().Contains(request.Location) ||
                                b.Address.City.Province.Region.Name.ToLower().Contains(request.Location) ||
                                b.Address.City.Province.Region.Country.Name.ToLower().Contains(request.Location)))
                                && (!request.Type.HasValue || b.PropertyTypeID == request.Type.Value)
                                && !b.IsDeleted,
                                i => i.Include(i => i.PropertyType)
                               .Include(i => i.Address)
                               .Include(i => i.PropertyDocuments.Where(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage))
                               .ThenInclude(x => x.Document));

            return await properties.ProjectTo<MapPropertyListDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }

}
