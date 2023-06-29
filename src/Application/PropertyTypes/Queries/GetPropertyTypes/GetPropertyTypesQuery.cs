using apollo.Application.PropertyTypes.Queries.DTOs;
using apollo.Domain.Entities.References;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.PropertyTypes.Queries.GetPropertyTypes
{
    public class GetPropertyTypesQuery : IRequest<IEnumerable<PropertyTypeDTO>>
    {
    }

    public class GetPropertyTypesQueryHandler : IRequestHandler<GetPropertyTypesQuery, IEnumerable<PropertyTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetPropertyTypesQueryHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<PropertyTypeDTO>> Handle(GetPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            var propertyTypes = _repository.Fetch<PropertyType>(x => !x.IsDeleted);
            return await propertyTypes.ProjectTo<PropertyTypeDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
