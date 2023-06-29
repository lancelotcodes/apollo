using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.References;

namespace apollo.Application.Countries.Commands.UpdateCountry
{
    public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;

        public UpdateCountryHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<int> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = _context.Countries.FirstOrDefault(i => i.ID == request.Id);
            if (country == null)
                throw new NotFoundException(nameof(Country), request.Id);

            country.Name = request.Name;
            country.IsDeleted = request.IsDeleted;

            _context.Countries.Update(country);
            await _context.SaveChangesAsync(cancellationToken);

            return country.ID;
        }
    }
}
