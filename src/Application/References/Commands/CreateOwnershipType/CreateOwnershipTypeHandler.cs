using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.References;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateOwnershipType
{
    public class CreateOwnershipTypeHandler : IRequestHandler<CreateOwnershipTypeCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateOwnershipTypeHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOwnershipTypeCommand request, CancellationToken cancellationToken)
        {
            OwnershipType ownershipType = new OwnershipType
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.OwnershipTypes.Add(ownershipType);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return ownershipType.ID;
        }
    }
}
