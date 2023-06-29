using apollo.Application.Common.Exceptions;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.PropertyMandate.Commands.SavePropertyMandate
{
    public class SavePropertyMandateHandler : IRequestHandler<SavePropertyMandateRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public SavePropertyMandateHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SavePropertyMandateRequest request, CancellationToken cancellationToken)
        {
            if (!request.Mandates.IsAny())
                throw new BadRequestException("Unable to create Mandate.");

            var property = _repository.GetById<Property>(request.Mandates.FirstOrDefault().PropertyID);

            if (property == null || property.IsDeleted)
            {
                throw new NotFoundException("Property not found.");
            }

            var mandates = _mapper.Map<List<Mandate>>(request.Mandates);
            if (mandates == null) throw new BadRequestException("Unable to create Mandate.");

            _repository.Save(mandates);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
