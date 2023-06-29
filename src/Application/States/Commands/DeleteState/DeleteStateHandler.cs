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

namespace apollo.Application.States.Commands.DeleteState
{
    public class DeleteStateHandler : IRequestHandler<DeleteStateCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DeleteStateHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            var state = _context.States.FirstOrDefault(i => i.ID == request.ID);
            if (state == null)
                throw new NotFoundException(nameof(State), request.ID);

            _context.States.Remove(state);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
