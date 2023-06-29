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
    public class GetCitiesByProvinceIDsQuery : IRequest<IEnumerable<CityDTO>>
    {
        public List<int> ProvinceIDs { get; set; }
    }

    public class GetCitiesByProvinceIDsQueryHandler : IRequestHandler<GetCitiesByProvinceIDsQuery, IEnumerable<CityDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetCitiesByProvinceIDsQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<CityDTO>> Handle(GetCitiesByProvinceIDsQuery request, CancellationToken cancellationToken)
        {
            if (request.ProvinceIDs == null)
            {
                return new List<CityDTO>();
            }
            var cities = _repository.Fetch<City>(g => request.ProvinceIDs.Contains(g.ProvinceID) && !g.IsDeleted);

            return await cities.ProjectTo<CityDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
