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

namespace apollo.Application.References.Commands.CreateTenantClassification
{
    public class CreateTenantClassificationHandler : IRequestHandler<CreateTenantClassificationCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTenantClassificationHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTenantClassificationCommand request, CancellationToken cancellationToken)
        {
            TenantClassification TenantClassification = new TenantClassification
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.TenantClassifications.Add(TenantClassification);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return TenantClassification.ID;
        }
    }
}
