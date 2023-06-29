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

namespace apollo.Application.References.Commands.CreateUnitStatus
{
    public class CreateUnitStatusHandler : IRequestHandler<CreateUnitStatusCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateUnitStatusHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateUnitStatusCommand request, CancellationToken cancellationToken)
        {
            UnitStatus unitStatus = new UnitStatus
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.UnitStatuses.Add(unitStatus);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return unitStatus.ID;
        }
    }
}
