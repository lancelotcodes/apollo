using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Application.States.Queries.GetStates;
using apollo.Domain.Entities.References;

namespace apollo.Application.States.Queries.GetStatesById
{
    public class GetStateByIdHandler : IRequestHandler<GetStateByIdQuery, StateDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetStateByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StateDTO> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            var state = _context.States
                .Where(x => x.ID == request.ID);

            if (state == null)
                throw new NotFoundException(nameof(State), request.ID);

            var data = await state
                .AsNoTracking()
                .ProjectTo<StateDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return data;
        }
    }
}
