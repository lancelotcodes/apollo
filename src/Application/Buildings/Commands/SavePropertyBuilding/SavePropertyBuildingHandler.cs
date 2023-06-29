using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using Shared.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Commands.SavePropertyBuilding
{
    public class SavePropertyBuildingHandler : IRequestHandler<SavePropertyBuildingRequest, SaveEntityResult>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public SavePropertyBuildingHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveEntityResult> Handle(SavePropertyBuildingRequest request, CancellationToken cancellationToken)
        {
            var building = new Building();

            if (request.ID > 0)
            {
                building = _repository.GetById<Building>(request.ID);
                if (building == null) throw new BadRequestException("Unable to save building.");
            }

            _mapper.Map(request, building);

            _repository.Save(building);
            await _repository.SaveChangesAsync();

            return new SaveEntityResult { EntityId = building.ID };
        }
    }
}
