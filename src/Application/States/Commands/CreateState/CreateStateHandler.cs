using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.References;

namespace apollo.Application.States.Commands.CreateState
{
    public class CreateStateHandler : IRequestHandler<CreateStateCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public CreateStateHandler(IApplicationDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            var state = _mapper.Map<State>(request);

            _context.States.Add(state);
            await _context.SaveChangesAsync(cancellationToken);

            return state.ID;
        }
    }
}
