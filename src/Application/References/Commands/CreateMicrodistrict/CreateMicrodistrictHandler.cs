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

namespace apollo.Application.References.Commands.CreateMicrodistrict
{
    public class CreateMicrodistrictHandler : IRequestHandler<CreateMicrodistrictCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateMicrodistrictHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMicrodistrictCommand request, CancellationToken cancellationToken)
        {
            MicroDistrict microDistrict = new MicroDistrict
            {
                Name = request.Name,
                CityID = request.CityID,
                PolygonPoints = JsonConvert.SerializeObject(request.PolygonPoints),
                IsDeleted = false
            };
            _context.MicroDistricts.Add(microDistrict);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return microDistrict.ID;
        }
    }
}
