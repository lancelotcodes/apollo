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

namespace apollo.Application.Lookups.GetProjectStatuses
{
    public class GetProjectStatusesQuery : IRequest<IEnumerable<ProjectStatusDTO>>
    {
    }

    public class GetProjectStatusesHandler : IRequestHandler<GetProjectStatusesQuery, IEnumerable<ProjectStatusDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetProjectStatusesHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<ProjectStatusDTO>> Handle(GetProjectStatusesQuery request, CancellationToken cancellationToken)
        {
            var cities = _repository.Fetch<ProjectStatus>(g => !g.IsDeleted);

            return await cities.ProjectTo<ProjectStatusDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
