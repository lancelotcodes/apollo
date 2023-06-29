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
using apollo.Domain.Entities.References;

namespace apollo.Application.States.Queries.GetStates
{
    public class GetStateHandler : IRequestHandler<GetStateQuery, IEnumerable<StateDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetStateHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StateDTO>> Handle(GetStateQuery request, CancellationToken cancellationToken)
        {
            var state = _context.States.Where(i => !i.IsDeleted && i.CountryID == request.CountryID);

            return await state
                .AsNoTracking()
                .ProjectTo<StateDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
