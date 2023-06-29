using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.References;
using AutoMapper;
using MediatR;
using Shared.Constants;
using Shared.Contracts;
using Shared.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Units.Commands.CreateUnit
{
    public class SaveUnitHandler : IRequestHandler<SaveUnitRequest, SaveEntityResult>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SaveUnitHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveEntityResult> Handle(SaveUnitRequest request, CancellationToken cancellationToken)
        {
            var floor = await _repository.UntrackFirstAsync<Floor>(x => x.ID == request.FloorID, i => i.Include(i => i.Building).ThenInclude(x => x.Property).ThenInclude(x => x.PropertyType));

            if (floor == null) throw new BadRequestException("Floor not found.");

            if (floor.Building == null) throw new BadRequestException("Building not found.");

            Domain.Entities.Core.Unit unit = new Domain.Entities.Core.Unit();

            if (request.ID > 0)
            {
                unit = _repository.First<Domain.Entities.Core.Unit>(x => x.ID == request.ID, i => i.Include(x => x.UnitStatus).Include(x => x.Property.Contracts));
                if (unit == null) throw new BadRequestException("Unit not found.");
                var unitStatus = _repository.First<UnitStatus>(x => x.ID == request.UnitStatusID);
                if (unit.UnitStatus.Name == AppUnitStatuses.Tenanted && unitStatus.Name == AppUnitStatuses.Vacant)
                {
                    unit.Property.Contracts.ToList().ForEach(x => x.IsHistorical = true);
                }
                _mapper.Map(request, unit);
            }
            else
            {
                ApplicationMapping.PropertyMappings.TryGetValue(floor.Building.Property.PropertyType.Name, out var propertyTypeName);

                if (string.IsNullOrEmpty(propertyTypeName))
                    throw new BadRequestException("Unable to save Unit");

                var unitPropertyType = await _repository.UntrackFirstAsync<PropertyType>(x => x.Name == propertyTypeName);

                if (unitPropertyType == null)
                    throw new BadRequestException("Unable to save Unit");

                _mapper.Map(request, unit);
                var property = new Property
                {
                    Name = $"{floor.Building.Property.Name} Unit-{floor.Name}",
                    PropertyTypeID = unitPropertyType.ID,
                    GradeID = floor.Building.Property.GradeID,
                    OwnerID = floor.Building.Property.OwnerID,
                    IsExclusive = floor.Building.Property.IsExclusive,
                    OwnerCompanyID = floor.Building.Property.OwnerCompanyID,
                    Note = floor.Building.Property.Note,
                };

                unit.Property = property;
            }
            _repository.Save(unit);
            await _repository.SaveChangesAsync();

            return new SaveEntityResult { EntityId = unit.ID };
        }
    }
}