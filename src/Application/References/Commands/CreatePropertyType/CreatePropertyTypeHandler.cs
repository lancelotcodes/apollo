using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.References;
using apollo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreatePropertyType
{
    public class CreatePropertyTypeHandler : IRequestHandler<CreatePropertyTypeCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePropertyTypeHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {

            //check for duplicate
            var existingProperty = await _context.PropertyTypes.FirstOrDefaultAsync(i => i.Name.ToLower() == request.Name.ToLower());

            if (existingProperty == null)
            {
                PropertyCategory category;
                Enum.TryParse(request.Category, out category);
                PropertyType propertyType = new PropertyType
                {
                    Name = request.Name,
                    Category = category,
                    IsDeleted = false
                };
                _context.PropertyTypes.Add(propertyType);
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (Exception err)
                {
                    throw new GenericException(err.Message);
                }
                return propertyType.ID;
            }
            else
            {
                //throw new GenericException($"A duplicate entry found with ID No. {existingProperty.ID}");
                return 0;
            }
        }
    }
}
