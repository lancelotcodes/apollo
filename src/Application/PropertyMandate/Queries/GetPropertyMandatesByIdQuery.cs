using apollo.Application.Common.Exceptions;
using apollo.Application.PropertyMandate.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace apollo.Application.PropertyMandate.Queries
{
    public class GetPropertyMandatesByIdQuery : IRequest<IEnumerable<MandateDTO>>
    {
        public int Id { get; set; }
    }

    public class GetPropertyMandatesHandler : IRequestHandler<GetPropertyMandatesByIdQuery, IEnumerable<MandateDTO>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetPropertyMandatesHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MandateDTO>> Handle(GetPropertyMandatesByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _repository.UntrackFirstAsync<Property>(x => x.ID == request.Id && !x.IsDeleted,
                           i => i.Include(i => i.Mandates).ThenInclude(y => y.Attachment));

            if (property == null)
                throw new NotFoundException();

            var agents = _mapper.Map<IEnumerable<MandateDTO>>(property.Mandates);
            return agents;
        }
    }
}
