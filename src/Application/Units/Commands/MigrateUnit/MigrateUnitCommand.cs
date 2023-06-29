using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Application.Units.Commands.CreateUnit;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Units.Commands.MigrateUnit
{
    public class MigrateUnitCommand : IRequest<MigrationResultModel>
    {
        public List<MigrateUnitDTO> Data { get; set; }

        public class Handler : IRequestHandler<MigrateUnitCommand, MigrationResultModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<MigrationResultModel> Handle(MigrateUnitCommand request, CancellationToken cancellationToken)
            {
                List<string> errors = new List<string>();

                var totalCount = request.Data.Count();
                var totalUploaded = 0;

                foreach (var data in request.Data)
                {
                    var refJSONString = JsonConvert.SerializeObject(data);

                    var csvEntity = JsonConvert.DeserializeObject<MigrateUnitDTO>(refJSONString);

                    if (string.IsNullOrEmpty(csvEntity.Name))
                    {
                        errors.Add($"Missing property name at row {request.Data.IndexOf(data) + 1}");
                    }

                    if (errors.Count() == 0)
                    {
                        if (!string.IsNullOrEmpty(csvEntity.Name))
                        {
                            try
                            {
                                var createCommand = new SaveUnitRequest
                                {
                                    Name = csvEntity.Name,
                                    FloorID = csvEntity.FloorID,
                                    PropertyID = csvEntity.PropertyID,
                                    UnitNumber = csvEntity.UnitNumber,
                                    UnitStatusID = csvEntity.UnitStatusID,
                                    AvailabilityID = csvEntity.AvailabilityID,
                                    LeaseFloorArea = csvEntity.LeaseFloorArea,
                                    ListingTypeID = csvEntity.ListingTypeID,
                                    BasePrice = csvEntity.BasePrice,
                                    CUSA = csvEntity.CUSA,
                                    HandOverConditionID = csvEntity.HandOverConditionID,
                                    ACCharges = csvEntity.ACCharges,
                                    ACExtensionCharges = csvEntity.ACExtensionCharges,
                                    EscalationRate = csvEntity.EscalationRate,
                                    MinimumLeaseTerm = csvEntity.MinimumLeaseTerm,
                                    ParkingRent = csvEntity.ParkingRent,
                                    HandOverDate = csvEntity.HandOverDate
                                };
                                var id = await _mediator.Send(createCommand);
                                totalUploaded++;
                            }
                            catch (Exception err)
                            {
                                errors.Add(err.Message);
                            }
                        }
                    }
                }

                return new MigrationResultModel
                {
                    Succeeded = errors.Count > 0 ? false : true,
                    Uploaded = totalUploaded,
                    Total = totalCount,
                    Errors = errors
                };
            }
        }

    }
}
