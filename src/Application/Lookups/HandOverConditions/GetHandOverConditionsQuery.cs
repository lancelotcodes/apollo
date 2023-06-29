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

namespace apollo.Application.Lookups.HandOverConditions
{
    public class GetHandOverConditionsQuery : IRequest<IEnumerable<HandOverConditionDTO>>
    {
    }

    public class GetHandOverConditionsQueryHandler : IRequestHandler<GetHandOverConditionsQuery, IEnumerable<HandOverConditionDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetHandOverConditionsQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<HandOverConditionDTO>> Handle(GetHandOverConditionsQuery request, CancellationToken cancellationToken)
        {
            var handOverConditions = _repository.Fetch<HandOverCondition>();

            return await handOverConditions.ProjectTo<HandOverConditionDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
