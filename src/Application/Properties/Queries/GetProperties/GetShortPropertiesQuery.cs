using apollo.Application.Properties.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace apollo.Application.Properties.Queries.GetProperties
{
    public class GetShortPropertiesQuery : IRequest<IEnumerable<ShortPropertyListDTO>>
    {
        public string Name { get; set; }
    }

    public class GetShortPropertiesHandler : IRequestHandler<GetShortPropertiesQuery, IEnumerable<ShortPropertyListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public GetShortPropertiesHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<ShortPropertyListDTO>> Handle(GetShortPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = _repository.Fetch<Property>(b => (string.IsNullOrWhiteSpace(request.Name) ||
                                b.Name.ToLower().Contains(request.Name))
                                && !b.IsDeleted && b.IsActive);

            return await properties.ProjectTo<ShortPropertyListDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
