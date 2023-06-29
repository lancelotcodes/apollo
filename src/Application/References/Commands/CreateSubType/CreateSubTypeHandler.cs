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

namespace apollo.Application.References.Commands.CreateSubType
{
    public class CreateSubTypeHandler : IRequestHandler<CreateSubTypeCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateSubTypeHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSubTypeCommand request, CancellationToken cancellationToken)
        {
            SubType subType = new SubType
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.SubTypes.Add(subType);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return subType.ID;
        }
    }
}
