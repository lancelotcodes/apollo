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

namespace apollo.Application.Lookups.GetOwnershipTypes
{
    public class GetOwnershipTypesQuery : IRequest<IEnumerable<OwnershipTypeDTO>>
    {
    }

    public class GetOwnershipTypesQueryHandler : IRequestHandler<GetOwnershipTypesQuery, IEnumerable<OwnershipTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetOwnershipTypesQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<OwnershipTypeDTO>> Handle(GetOwnershipTypesQuery request, CancellationToken cancellationToken)
        {
            var OwnershipTypes = _repository.Fetch<OwnershipType>(g => !g.IsDeleted);

            return await OwnershipTypes.ProjectTo<OwnershipTypeDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
