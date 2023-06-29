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

namespace apollo.Application.Lookups.TenantClassifications
{
    public class GetTenantClassificationsQuery : IRequest<IEnumerable<TenantClassificationDTO>>
    {
    }

    public class GetTenantClassificationsQueryHandler : IRequestHandler<GetTenantClassificationsQuery, IEnumerable<TenantClassificationDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetTenantClassificationsQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<TenantClassificationDTO>> Handle(GetTenantClassificationsQuery request, CancellationToken cancellationToken)
        {
            var availabilities = _repository.Fetch<TenantClassification>();

            return await availabilities.ProjectTo<TenantClassificationDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
