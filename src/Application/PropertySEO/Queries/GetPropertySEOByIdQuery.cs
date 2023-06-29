using apollo.Application.Common.Exceptions;
using apollo.Application.PropertySEO.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.PropertySEO.Queries
{
    public class GetPropertySEOByIdQuery : IRequest<SEODTO>
    {
        public int Id { get; set; }
    }

    public class GetPropertySEOHandler : IRequestHandler<GetPropertySEOByIdQuery, SEODTO>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GetPropertySEOHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<SEODTO> Handle(GetPropertySEOByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.UntrackFirstAsync<Property>(x => x.ID == request.Id && !x.IsDeleted,
                          i => i.Include(i => i.SEO));

            if (property.SEO == null)
                throw new NotFoundException();

            return _mapper.Map<SEODTO>(property);
        }
    }
}
