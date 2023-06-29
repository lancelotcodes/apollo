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

namespace apollo.Application.References.Commands.CreateCity
{
    public class CreateCityHandler : IRequestHandler<CreateCityCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCityHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City city = new City
            {
                Name = request.Name,
                ProvinceID = request.ProvinceID,
                PolygonPoints = JsonConvert.SerializeObject(request.PolygonPoints),
                IsDeleted = false
            };
            _context.Cities.Add(city);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return city.ID;
        }
    }
}
