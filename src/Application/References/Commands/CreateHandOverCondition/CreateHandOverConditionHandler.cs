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

namespace apollo.Application.References.Commands.CreateHandOverCondition
{
    public class CreateHandOverConditionHandler : IRequestHandler<CreateHandOverConditionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateHandOverConditionHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateHandOverConditionCommand request, CancellationToken cancellationToken)
        {
            HandOverCondition handoverCondition = new HandOverCondition
            {
                Name = request.Name,
                IsDeleted = false
            };
            _context.HandOverConditions.Add(handoverCondition);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return handoverCondition.ID;
        }
    }
}
