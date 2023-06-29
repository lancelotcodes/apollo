using apollo.Application.Lookups.DTOs;
using apollo.Domain.Entities.References;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Lookups.GetCities
{
    public class GetCitiesQuery : IRequest<IEnumerable<CityDTO>>
    {
        public int? ProvinceID { get; set; } = null;
    }

    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IEnumerable<CityDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetCitiesQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<CityDTO>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = _repository.Fetch<City>(g => !g.IsDeleted);

            if (request.ProvinceID.HasValue)
            {
                cities = cities.Where(x => x.ProvinceID == request.ProvinceID);
            }

            return await cities.ProjectTo<CityDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }

}
