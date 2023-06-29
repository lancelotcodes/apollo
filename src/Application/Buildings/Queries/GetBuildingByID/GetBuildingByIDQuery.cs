using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Queries.GetBuildingByID
{
    public class GetBuildingByIDQuery : IRequest<BuildingDTO>
    {
        public int ID { get; set; }
    }

    public class GetBuildingByIDHandler : IRequestHandler<GetBuildingByIDQuery, BuildingDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public GetBuildingByIDHandler(IApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BuildingDTO> Handle(GetBuildingByIDQuery request, CancellationToken cancellationToken)
        {
            var building = _context.Buildings
                .Include(i => i.Property)
                    .ThenInclude(i => i.SEO)
                .Include(i => i.Property)
                    .ThenInclude(i => i.Address)
                .Include(i => i.Property)
                    .ThenInclude(i => i.PropertyDocuments)
                .Include(i => i.Property)
                    .ThenInclude(i => i.Agents)
                .Include(i => i.Floors)
                    .ThenInclude(i => i.Units)
                        .ThenInclude(i => i.Property)
                            .ThenInclude(i => i.Contracts)
                                .ThenInclude(i => i.Company)
                .Include(i => i.Floors)
                    .ThenInclude(i => i.Units)
                        .ThenInclude(i => i.Property)
                            .ThenInclude(i => i.Contracts)
                                .ThenInclude(i => i.Contact)
                .Include(i => i.Floors)
                    .ThenInclude(i => i.Units)
                        .ThenInclude(i => i.Property)
                            .ThenInclude(i => i.Contracts)
                                .ThenInclude(i => i.Broker)
                .Include(i => i.Floors)
                    .ThenInclude(i => i.Units)
                        .ThenInclude(i => i.Property)
                            .ThenInclude(i => i.Contracts)
                                .ThenInclude(i => i.BrokerCompany)
                .Where(i => i.ID == request.ID);
            var mappedData = await building
                .AsNoTracking()
                .ProjectTo<BuildingDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return mappedData;
        }
    }
}
