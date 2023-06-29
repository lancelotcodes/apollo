using apollo.Application.Agents.Queries.DTOs;
using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace apollo.Application.Agents.Queries.GetAgents
{
    public class GetPropertyAgentsByIdQuery : IRequest<IEnumerable<PropertyAgentDTO>>
    {
        public int Id { get; set; }
    }

    public class GetPropertyAgentsHandler : IRequestHandler<GetPropertyAgentsByIdQuery, IEnumerable<PropertyAgentDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetPropertyAgentsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyAgentDTO>> Handle(GetPropertyAgentsByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.UntrackFirstAsync<Property>(x => x.ID == request.Id && !x.IsDeleted,
                              i => i.Include(i => i.Agents.Where(x => !x.IsDeleted)).ThenInclude(y => y.Agent));

            if (property == null)
                throw new NotFoundException();

            var agents = _mapper.Map<IEnumerable<PropertyAgentDTO>>(property.Agents);
            return agents;
        }
    }
}
