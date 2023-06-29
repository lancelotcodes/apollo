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

namespace apollo.Application.References.Commands.CreateSubMarket
{
    public class CreateSubMarketHandler : IRequestHandler<CreateSubMarketCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateSubMarketHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSubMarketCommand request, CancellationToken cancellationToken)
        {
            SubMarket subMarket = new SubMarket
            {
                Name = request.Name,
                CityID = request.CityID,
                PolygonPoints = JsonConvert.SerializeObject(request.PolygonPoints),
                IsDeleted = false
            };
            _context.SubMarkets.Add(subMarket);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return subMarket.ID;
        }
    }
}
