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
    public class GetMicroDistrictsQuery : IRequest<IEnumerable<MicroDistrictDTO>>
    {
        public int CityID { get; set; }
    }

    public class GetMicroDistrictsQueryHandler : IRequestHandler<GetMicroDistrictsQuery, IEnumerable<MicroDistrictDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetMicroDistrictsQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<MicroDistrictDTO>> Handle(GetMicroDistrictsQuery request, CancellationToken cancellationToken)
        {
            var MicroDistricts = _repository.Fetch<MicroDistrict>(g => g.CityID == request.CityID && !g.IsDeleted);

            return await MicroDistricts.ProjectTo<MicroDistrictDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
