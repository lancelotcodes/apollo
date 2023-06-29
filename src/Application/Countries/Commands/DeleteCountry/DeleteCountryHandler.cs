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

namespace apollo.Application.Countries.Commands.DeleteCountry
{
    public class DeleteCountryHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DeleteCountryHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var country = _context.Countries.FirstOrDefault(i => i.ID == request.Id);
            if (country == null)
                throw new NotFoundException(nameof(Country), request.Id);

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
