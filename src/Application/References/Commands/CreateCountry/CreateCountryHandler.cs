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

namespace apollo.Application.References.Commands.CreateCountry
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCountryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = new Country
            {
                Name = request.Name,
                PolygonPoints = JsonConvert.SerializeObject(request.PolygonPoints),
                IsDeleted = false
            };
            _context.Countries.Add(country);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return country.ID;
        }
    }
}