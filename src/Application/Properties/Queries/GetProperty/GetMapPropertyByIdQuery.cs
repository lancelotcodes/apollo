using apollo.Application.Common.Exceptions;
using apollo.Application.Properties.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Queries.GetProperty
{
    public class GetMapPropertyByIdQuery : IRequest<MapPropertyDTO>
    {
        public int Id { get; set; }
    }

    public class GetMapPropertyHandler : IRequestHandler<GetMapPropertyByIdQuery, MapPropertyDTO>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public GetMapPropertyHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<MapPropertyDTO> Handle(GetMapPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.UntrackFirstAsync<Property>(x => x.ID == request.Id && !x.IsDeleted,
                               i => i.Include(i => i.PropertyType)
                               .Include(i => i.Address)
                               .Include(i => i.Building).ThenInclude(i => i.Developer)
                               .Include(i => i.Building).ThenInclude(i => i.LeasingContact));

            if (property == null)
                throw new NotFoundException();

            return _mapper.Map<MapPropertyDTO>(property);
        }
    }

}
