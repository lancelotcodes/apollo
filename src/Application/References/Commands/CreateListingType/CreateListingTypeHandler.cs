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

namespace apollo.Application.References.Commands.CreateListingType
{
    public class CreateListingTypeHandler : IRequestHandler<CreateListingTypeCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateListingTypeHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateListingTypeCommand request, CancellationToken cancellationToken)
        {
            ListingType listingType = new ListingType
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.ListingTypes.Add(listingType);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return listingType.ID;
        }
    }
}
