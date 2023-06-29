using apollo.Application.Buildings.Commands.CreateBuilding;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Commands.MigrateBuilding
{
    public class MigrateBuildingCommand : IRequest<MigrationResultModel>
    {
        public List<MigrateBuildingsDTO> Data { get; set; }

        public class Handler : IRequestHandler<MigrateBuildingCommand, MigrationResultModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<MigrationResultModel> Handle(MigrateBuildingCommand request, CancellationToken cancellationToken)
            {
                List<string> errors = new List<string>();

                var totalCount = request.Data.Count();
                var totalUploaded = 0;

                foreach (var data in request.Data)
                {
                    var refJSONString = JsonConvert.SerializeObject(data);

                    var csvEntity = JsonConvert.DeserializeObject<MigrateBuildingsDTO>(refJSONString);

                    if (String.IsNullOrEmpty(csvEntity.Name))
                    {
                        errors.Add($"Missing property name at row {request.Data.IndexOf(data) + 1}");
                    }

                    if (errors.Count() == 0)
                    {
                        if (!string.IsNullOrEmpty(csvEntity.Name))
                        {
                            try
                            {
                                var createCommand = new CreateBuildingCommand
                                {
                                    Name = csvEntity.Name,
                                    PropertyName = csvEntity.PropertyName,
                                    PropertyTypeID = 0,
                                    PropertyTypeName = csvEntity.PropertyTypeName,
                                    SubTypeID = 0,
                                    SubTypeName = null,
                                    MasterProjectID = 0,
                                    MasterProjectName = null,
                                    GradeID = 0,
                                    GradeName = csvEntity.GradeName,
                                    Note = csvEntity.Note,
                                    IsExclusive = csvEntity.IsExclusive,
                                    SortOrder = csvEntity.SortOrder,
                                    MainImage = csvEntity.MainImage,
                                    MainFloorPlanImage = csvEntity.MainFloorPlanImage,

                                    // Location Info
                                    Line1 = csvEntity.Line1,
                                    Line2 = csvEntity.Line2,
                                    ZipCode = csvEntity.ZipCode,
                                    CityID = 0,
                                    CityName = csvEntity.CityName,
                                    SubMarketID = 0,
                                    SubMarketName = csvEntity.SubMarketName,
                                    MicroDistrictID = 0,
                                    MicroDistrictName = csvEntity.MicroDistrictName,
                                    Latitude = csvEntity.Latitude,
                                    Longitude = csvEntity.Longitude,
                                    PolygonPoints = csvEntity.PolygonPoints,

                                    // Company and Contacts Info
                                    DeveloperID = 0,
                                    DeveloperName = csvEntity.DeveloperName,
                                    LeasingContactID = 0,
                                    LeasingContactName = csvEntity.LeasingContactName,
                                    PropertyManagementID = 0,
                                    PropertyManagementName = csvEntity.PropertyManagementName,
                                    PropertyOwnerID = 0,
                                    PropertyOwnerName = csvEntity.PropertyOwnerName,
                                    PropertyOwnerCompanyID = 0,
                                    PropertyOwnerCompany = csvEntity.PropertyOwnerCompany,
                                    PropertyContactID = 0,
                                    PropertyContactName = csvEntity.PropertyContactName,
                                    PropertyContactCompanyID = 0,
                                    PropertyContactCompany = csvEntity.PropertyContactCompany,

                                    // SEO Info
                                    SEOID = 0,
                                    Url = csvEntity.Url,
                                    PageTitle = csvEntity.PageTitle,
                                    PageDescription = csvEntity.PageDescription,
                                    MetaKeyword = csvEntity.MetaKeyword,
                                    PublishedDate = csvEntity.PublishedDate,
                                    IsPublished = csvEntity.IsPublished,
                                    IsFeatured = csvEntity.IsFeatured,
                                    FeaturedWeight = csvEntity.FeaturedWeight,
                                    PublishType = Domain.Enums.PublishType.Both,

                                    // Building Info
                                    ACSystem = csvEntity.ACSystem,
                                    Amenities = csvEntity.Amenities,
                                    CeilingHeight = csvEntity.CeilingHeight,
                                    DateBuilt = csvEntity.DateBuilt,
                                    //DensityRatio = csvEntity.DensityRatio,
                                    EfficiencyRatio = csvEntity.EfficiencyRatio,
                                    Elevators = csvEntity.Elevators,
                                    Floors = csvEntity.Floors,
                                    GrossBuildingSize = csvEntity.GrossBuildingSize,
                                    GrossLeasableSize = csvEntity.GrossLeasableSize,
                                    LEED = csvEntity.LEED,
                                    MinimumLeaseTerm = csvEntity.MinimumLeaseTerm,
                                    OperatingHours = csvEntity.OperatingHours,
                                    OwnershipTypeID = 0,
                                    OwnershipTypeName = csvEntity.OwnershipTypeName,
                                    ParkingElevator = csvEntity.ParkingElevator,
                                    PassengerElevator = csvEntity.PassengerElevator,
                                    PEZA = csvEntity.PEZA,
                                    Power = csvEntity.Power,
                                    ProjectStatusID = 0,
                                    ProjectStatusName = csvEntity.ProjectStatusName,
                                    TypicalFloorPlateSize = csvEntity.TypicalFloorPlateSize,
                                    ServiceElevator = csvEntity.ServiceElevator,
                                    Telcos = csvEntity.Telcos,
                                    TenantMix = csvEntity.TenantMix,
                                    TotalFloors = csvEntity.TotalFloors,
                                    TotalUnits = csvEntity.TotalUnits,
                                    TurnOverDate = csvEntity.TurnOverDate,
                                    WebPage = csvEntity.WebPage,
                                    YearBuilt = csvEntity.YearBuilt
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
