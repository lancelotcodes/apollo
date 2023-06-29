using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.References;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateRegion
{
    public class CreateRegionHandler : IRequestHandler<CreateRegionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateRegionHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = new Region
            {
                Name = request.Name,
                CountryID = request.CountryID,
                PolygonPoints = JsonConvert.SerializeObject(request.PolygonPoints),
                IsDeleted = false
            };
            _context.Regions.Add(region);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return region.ID;
        }
    }
}
