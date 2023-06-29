using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.References;

namespace apollo.Application.States.Commands.UpdateState
{
    public class UpdateStateHandler : IRequestHandler<UpdateStateCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public UpdateStateHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
        {
            var state = _context.States.FirstOrDefault(i => i.ID == request.ID);
            if (state == null)
                throw new NotFoundException(nameof(State), request.ID);

            state.Name = request.Name;
            state.IsDeleted = request.IsDeleted;

            _context.States.Update(state);
            await _context.SaveChangesAsync(cancellationToken);

            return state.ID;
        }
    }
}
