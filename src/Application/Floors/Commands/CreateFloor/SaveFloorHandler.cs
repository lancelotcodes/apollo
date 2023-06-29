using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.References;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Contracts;
using Shared.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Floors.Commands.CreateFloor
{
    public class SaveFloorHandler : IRequestHandler<SaveFloorRequest, SaveEntityResult>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SaveFloorHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SaveEntityResult> Handle(SaveFloorRequest request, CancellationToken cancellationToken)
        {
            var building = await _repository.UntrackFirstAsync<Building>(x => x.ID == request.BuildingID, i => i.Include(i => i.Property).ThenInclude(x => x.PropertyType));

            Floor floor = new Floor();

            if (request.ID > 0)
            {
                floor = _repository.GetById<Floor>(request.ID);
                if (floor == null) throw new BadRequestException("Floor not found.");
                _mapper.Map(request, floor);
            }
            else
            {
                ApplicationMapping.PropertyMappings.TryGetValue(building.Property.PropertyType.Name, out var propertyTypeName);

                if (string.IsNullOrEmpty(propertyTypeName))
                    throw new BadRequestException("Unable to save Floor");

                var unitPropertyType = await _repository.UntrackFirstAsync<PropertyType>(x => x.Name == propertyTypeName);

                if (unitPropertyType == null)
                    throw new BadRequestException("Unable to save Floor");

                _mapper.Map(request, floor);
                var property = new Property
                {
                    Name = $"{building.Property.Name} Unit-{floor.Name}",
                    PropertyTypeID = unitPropertyType.ID,
                    GradeID = building.Property.GradeID,
                    OwnerID = building.Property.OwnerID,
                    IsExclusive = building.Property.IsExclusive,
                    OwnerCompanyID = building.Property.OwnerCompanyID,
                    Note = building.Property.Note,
                };

                var defaultUnit = new Domain.Entities.Core.Unit()
                {
                    Name = "1",
                    UnitNumber = $"{floor.Name}-01",
                    AvailabilityID = 1,
                    HandOverConditionID = 7,
                    IsActive = true,
                    Property = property,
                    UnitStatusID = 1,
                    LeaseFloorArea = floor.FloorPlateSize,
                    ListingTypeID = 2,
                    BasePrice = 1000,
                    CUSA = 165,
                    EscalationRate = 5,
                    MinimumLeaseTerm = 36,
                    ParkingRent = 500,
                };

                floor.Units.Add(defaultUnit);
            }

            _repository.Save(floor);
            await _repository.SaveChangesAsync();

            return new SaveEntityResult { EntityId = floor.ID };
        }
    }
}
