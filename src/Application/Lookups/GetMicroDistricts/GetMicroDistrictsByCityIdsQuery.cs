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

namespace apollo.Application.Lookups.GetMicroDistricts
{
    public class GetMicroDistrictsByCityIdsQuery : IRequest<IEnumerable<MicroDistrictDTO>>
    {
        public List<int> CityIDs { get; set; }
    }

    public class GetMicroDistrictsByCityIdsQueryHandler : IRequestHandler<GetMicroDistrictsByCityIdsQuery, IEnumerable<MicroDistrictDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetMicroDistrictsByCityIdsQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<MicroDistrictDTO>> Handle(GetMicroDistrictsByCityIdsQuery request, CancellationToken cancellationToken)
        {
            if (request.CityIDs == null)
            {
                return new List<MicroDistrictDTO>();
            }

            var microDistricts = _repository.Fetch<MicroDistrict>(g => request.CityIDs.Contains(g.CityID) && !g.IsDeleted);

            return await microDistricts.ProjectTo<MicroDistrictDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
