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

namespace apollo.Application.References.Commands.CreateProvince
{
    public class CreateProvinceHandler : IRequestHandler<CreateProvinceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProvinceHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
        {
            Province province = new Province
            {
                Name = request.Name,
                RegionID = request.RegionID,
                PolygonPoints = JsonConvert.SerializeObject(request.PolygonPoints),
                IsDeleted = false
            };
            _context.Provinces.Add(province);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return province.ID;
        }
    }
}