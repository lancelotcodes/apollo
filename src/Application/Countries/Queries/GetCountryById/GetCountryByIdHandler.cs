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

namespace apollo.Application.Countries.Queries.GetCountryById
{
    public class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, CountryDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCountryByIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryDTO> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var country = _context.Countries
                .Where(x => x.ID == request.Id);

            if (country == null)
                throw new NotFoundException(nameof(Country), request.Id);

            var data = await country
                .AsNoTracking()
                .ProjectTo<CountryDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return data;
        }
    }
}
