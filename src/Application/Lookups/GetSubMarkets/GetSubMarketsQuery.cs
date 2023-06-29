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
    public class GetSubMarketsQuery : IRequest<IEnumerable<SubMarketDTO>>
    {
        public int CityID { get; set; }
    }

    public class GetSubMarketsQueryHandler : IRequestHandler<GetSubMarketsQuery, IEnumerable<SubMarketDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetSubMarketsQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<SubMarketDTO>> Handle(GetSubMarketsQuery request, CancellationToken cancellationToken)
        {
            var SubMarkets = _repository.Fetch<SubMarket>(g => g.CityID == request.CityID && !g.IsDeleted);

            return await SubMarkets.ProjectTo<SubMarketDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
