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
using apollo.Application.Common.Interfaces;
using apollo.Application.Countries.Queries.GetCountryById;

namespace apollo.Application.Countries.Queries.GetCountryList
{
    public class GetCountryListHandler : IRequestHandler<GetCountryListQuery, IEnumerable<CountryDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCountryListHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDTO>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var country = _context.Countries.Where(i => !i.IsDeleted);

            return await country
                .AsNoTracking()
                .ProjectTo<CountryDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
