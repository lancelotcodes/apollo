using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Application.References.Commands.CreateCity;
using apollo.Application.References.Commands.CreateCountry;
using apollo.Application.References.Commands.CreateGrade;
using apollo.Application.References.Commands.CreateHandOverCondition;
using apollo.Application.References.Commands.CreateListingType;
using apollo.Application.References.Commands.CreateMicrodistrict;
using apollo.Application.References.Commands.CreateOwnershipType;
using apollo.Application.References.Commands.CreateProjectStatus;
using apollo.Application.References.Commands.CreatePropertyType;
using apollo.Application.References.Commands.CreateProvince;
using apollo.Application.References.Commands.CreateRegion;
using apollo.Application.References.Commands.CreateSubMarket;
using apollo.Application.References.Commands.CreateSubType;
using apollo.Application.References.Commands.CreateTenantClassification;
using apollo.Application.References.Commands.CreateUnitStatus;
using apollo.Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.MigrateReferences
{
    public class MigrateReferencesCommand : IRequest<MigrationResultModel>
    {
        public List<MigrateReferencesDTO> Data { get; set; }

        public class Handler : IRequestHandler<MigrateReferencesCommand, MigrationResultModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<MigrationResultModel> Handle(MigrateReferencesCommand request, CancellationToken cancellationToken)
            {
                List<string> errors = new List<string>();

                var totalCount = request.Data.Count();
                var totalUploaded = 0;

                foreach (var data in request.Data)
                {
                    var refJSONString = JsonConvert.SerializeObject(data);

                    var csvEntity = JsonConvert.DeserializeObject<MigrateReferencesDTO>(refJSONString);

                    if (String.IsNullOrEmpty(csvEntity.Name))
                    {
                        errors.Add($"Missing property name at row {request.Data.IndexOf(data) + 1}");
                    }

                    if (errors.Count() == 0)
                    {
                        if ((!string.IsNullOrEmpty(csvEntity.Name)) && (!string.IsNullOrEmpty(csvEntity.Entity)))
                        {

                            #region Property Type
                            if (csvEntity.Entity.ToLower() == "propertytype")
                            {
                                try
                                {
                                    var createCommand = new CreatePropertyTypeCommand
                                    {
                                        Name = csvEntity.Name,
                                        Category = csvEntity.ParentEntity
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Grade
                            if (csvEntity.Entity.ToLower() == "grade")
                            {
                                try
                                {
                                    int propertyTypeID = _context.PropertyTypes.FirstOrDefault(i => i.Name.ToLower() == csvEntity.ParentEntity.ToLower()).ID;
                                    var createCommand = new CreateGradeCommand
                                    {
                                        Name = csvEntity.Name,
                                        PropertyTypeID = propertyTypeID
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Hand Over Condition
                            if (csvEntity.Entity.ToLower() == "handovercondition")
                            {
                                try
                                {
                                    var createCommand = new CreateHandOverConditionCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Listing Type
                            if (csvEntity.Entity.ToLower() == "listingtype")
                            {
                                try
                                {
                                    var createCommand = new CreateListingTypeCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Ownership Type
                            if (csvEntity.Entity.ToLower() == "ownershiptype")
                            {
                                try
                                {
                                    var createCommand = new CreateOwnershipTypeCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Project Status
                            if (csvEntity.Entity.ToLower() == "projectstatus")
                            {
                                try
                                {
                                    var createCommand = new CreateProjectStatusCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Sub Type
                            if (csvEntity.Entity.ToLower() == "subtype")
                            {
                                try
                                {
                                    var createCommand = new CreateSubTypeCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Tenant Classification
                            if (csvEntity.Entity.ToLower() == "tenantclassification")
                            {
                                try
                                {
                                    var createCommand = new CreateTenantClassificationCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Unit Status
                            if (csvEntity.Entity.ToLower() == "unitstatus")
                            {
                                try
                                {
                                    var createCommand = new CreateUnitStatusCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Country
                            if (csvEntity.Entity.ToLower() == "country")
                            {
                                try
                                {
                                    var createCommand = new CreateCountryCommand
                                    {
                                        Name = csvEntity.Name
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Region
                            if (csvEntity.Entity.ToLower() == "region")
                            {
                                try
                                {
                                    int countryID = _context.Countries.FirstOrDefault(i => i.Name.ToLower() == csvEntity.ParentEntity.ToLower()).ID;
                                    var createCommand = new CreateRegionCommand
                                    {
                                        Name = csvEntity.Name,
                                        PolygonPoints = csvEntity.PolygonPoints,
                                        CountryID = countryID
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Province
                            if (csvEntity.Entity.ToLower() == "province")
                            {
                                try
                                {
                                    int regionID = _context.Regions.FirstOrDefault(i => i.Name.ToLower() == csvEntity.ParentEntity.ToLower()).ID;
                                    var createCommand = new CreateProvinceCommand
                                    {
                                        Name = csvEntity.Name,
                                        PolygonPoints = csvEntity.PolygonPoints,
                                        RegionID = regionID
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region City
                            if (csvEntity.Entity.ToLower() == "city")
                            {
                                try
                                {
                                    int provinceID = _context.Provinces.FirstOrDefault(i => i.Name.ToLower() == csvEntity.ParentEntity.ToLower()).ID;
                                    var createCommand = new CreateCityCommand
                                    {
                                        Name = csvEntity.Name,
                                        PolygonPoints = csvEntity.PolygonPoints,
                                        ProvinceID = provinceID
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region SubMarket
                            if (csvEntity.Entity.ToLower() == "submarket")
                            {
                                try
                                {
                                    int cityID = _context.Cities.FirstOrDefault(i => i.Name.ToLower() == csvEntity.ParentEntity.ToLower()).ID;
                                    var createCommand = new CreateSubMarketCommand
                                    {
                                        Name = csvEntity.Name,
                                        PolygonPoints = csvEntity.PolygonPoints,
                                        CityID = cityID
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion

                            #region Microdistrict
                            if (csvEntity.Entity.ToLower() == "microdistrict")
                            {
                                try
                                {
                                    int cityID = _context.Cities.FirstOrDefault(i => i.Name.ToLower() == csvEntity.ParentEntity.ToLower()).ID;
                                    var createCommand = new CreateMicrodistrictCommand
                                    {
                                        Name = csvEntity.Name,
                                        PolygonPoints = csvEntity.PolygonPoints,
                                        CityID = cityID
                                    };
                                    var id = await _mediator.Send(createCommand);
                                    totalUploaded++;
                                }
                                catch (Exception err)
                                {
                                    errors.Add(err.Message);
                                }
                            }
                            #endregion
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