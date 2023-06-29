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

namespace apollo.Application.Lookups.GetSubMarkets
{
    public class GetSubMarketsByCityIdsQuery : IRequest<IEnumerable<SubMarketDTO>>
    {
        public List<int> CityIDs { get; set; }
    }

    public class GetSubMarketsByCityIdsQueryHandler : IRequestHandler<GetSubMarketsByCityIdsQuery, IEnumerable<SubMarketDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetSubMarketsByCityIdsQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<SubMarketDTO>> Handle(GetSubMarketsByCityIdsQuery request, CancellationToken cancellationToken)
        {
            var SubMarkets = _repository.Fetch<SubMarket>(g => request.CityIDs.Contains(g.CityID) && !g.IsDeleted);

            return await SubMarkets.ProjectTo<SubMarketDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
