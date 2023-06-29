using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Properties.Commands.SaveProperty
{
    public class SavePropertyHandler : IRequestHandler<SavePropertyRequest, SaveEntityResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public SavePropertyHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveEntityResult> Handle(SavePropertyRequest request, CancellationToken cancellationToken)
        {
            var property = new Property();

            if (request.ID > 0)
            {
                property = _repository.GetById<Property>(request.ID);
                if (property == null) throw new BadRequestException("Unable to create Property.");
            }
            else
            {
                property.IsActive = false;
            }

            _mapper.Map(request, property);

            _repository.Save(property);
            await _repository.SaveChangesAsync();

            return new SaveEntityResult { EntityId = property.ID };
        }
    }
}
