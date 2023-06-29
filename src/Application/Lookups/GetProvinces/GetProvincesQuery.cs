using apollo.Application.Lookups.DTOs;
using apollo.Domain.Entities.References;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Shared.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Lookups.GetCities
{
    public class GetProvincesQuery : IRequest<IEnumerable<ProvinceDTO>>
    {
    }

    public class GetProvincesQueryHandler : IRequestHandler<GetProvincesQuery, IEnumerable<ProvinceDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetProvincesQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<ProvinceDTO>> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
        {
            return _repository.Fetch<Province>(g => !g.IsDeleted).ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider).AsEnumerable();
        }
    }
}
