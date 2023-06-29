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

namespace apollo.Application.Countries.Commands.CreateCountry
{
    public class AddCountryHandler : IRequestHandler<AddCountryCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddCountryHandler(IApplicationDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var country = _mapper.Map<Country>(request);
            
            _context.Countries.Add(country);
            await _context.SaveChangesAsync(cancellationToken);

            return country.ID;
        }
    }

}
