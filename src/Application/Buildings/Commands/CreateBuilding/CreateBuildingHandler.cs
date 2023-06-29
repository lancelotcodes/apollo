using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.Shared;
using apollo.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Commands.CreateBuilding
{
    public class CreateBuildingHandler : IRequestHandler<CreateBuildingCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateBuildingHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
        {
            int sortOrder = 1;
            Building existingBuilding = null;
            Property existingProperty = null;
            try
            {
                existingBuilding = await _context.Buildings.FirstOrDefaultAsync(i => i.Name.ToLower() == request.Name.ToLower());
                existingProperty = await _context.Properties.FirstOrDefaultAsync(i => i.Name.ToLower() == request.Name.ToLower());
            }
            catch
            {

            }

            if (existingBuilding != null)
            {
                throw new GenericException($"A duplicate entry found with Building ID No. {existingProperty.ID}");
            }

            if (existingProperty != null)
            {
                throw new GenericException($"A duplicate entry found with Property ID No. {existingProperty.ID}");
            }

            List<PropertyAgent> agents = new List<PropertyAgent>();

            if (request.PropertyAgents != null)
            {
                if (request.PropertyAgents.Count() > 0)
                {
                    foreach (var item in request.PropertyAgents)
                    {
                        PropertyAgent agent = new PropertyAgent
                        {
                            AgentID = item.AgentID,
                            AgentType = item.AgentType,
                            IsVisibleOnWeb = item.IsVisibleOnWeb,
                            IsDeleted = false
                        };
                        agents.Add(agent);
                    }
                }
            }

            if ((request.CityID == 0) && (!string.IsNullOrEmpty(request.CityName)))
            {
                var city = await _context.Cities.FirstOrDefaultAsync(i => i.Name.ToLower() == request.CityName.ToLower());
                request.CityID = city.ID;
            }

            if ((request.SubMarketID == 0) && (!string.IsNullOrEmpty(request.SubMarketName)))
            {
                var submarket = await _context.SubMarkets.FirstOrDefaultAsync(i => i.Name.ToLower() == request.SubMarketName.ToLower());
                request.SubMarketID = submarket.ID;
            }

            if ((request.MicroDistrictID == 0) && (!string.IsNullOrEmpty(request.MicroDistrictName)))
            {
                var microD = await _context.MicroDistricts.FirstOrDefaultAsync(i => i.Name.ToLower() == request.MicroDistrictName.ToLower());
                request.MicroDistrictID = microD.ID;
            }

            if ((request.PropertyTypeID == 0) && (!string.IsNullOrEmpty(request.PropertyTypeName)))
            {
                var propertyType = await _context.PropertyTypes.FirstOrDefaultAsync(i => i.Name.ToLower() == request.PropertyTypeName.ToLower());
                request.PropertyTypeID = propertyType.ID;
            }

            if ((request.GradeID == 0) && (!string.IsNullOrEmpty(request.GradeName)))
            {
                var grade = await _context.Grades.FirstOrDefaultAsync(i => i.Name.ToLower() == request.GradeName.ToLower());
                request.GradeID = grade.ID;
            }

            if ((request.ProjectStatusID == 0) && (!string.IsNullOrEmpty(request.ProjectStatusName)))
            {
                var project = await _context.ProjectStatuses.FirstOrDefaultAsync(i => i.Name.ToLower() == request.ProjectStatusName.ToLower());
                request.ProjectStatusID = project.ID;
            }

            if ((request.OwnershipTypeID == 0) && (!string.IsNullOrEmpty(request.OwnershipTypeName)))
            {
                var ownershipType = await _context.OwnershipTypes.FirstOrDefaultAsync(i => i.Name.ToLower() == request.OwnershipTypeName.ToLower());
                request.OwnershipTypeID = ownershipType.ID;
            }

            PEZAStatus pezaStatus;
            Enum.TryParse(request.PEZA, out pezaStatus);

            Address address = new Address
            {
                Line1 = request.Line1,
                Line2 = request.Line2,
                CityID = request.CityID,
                SubMarketID = (request.SubMarketID != 0) ? request.SubMarketID : null,
                MicroDistrictID = (request.MicroDistrictID != 0) ? request.MicroDistrictID : null,
                ZipCode = request.ZipCode,
                AddressTag = AddressTag.Primary,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                PolygonPoints = JsonConvert.SerializeObject(request.PolygonPoints)
            };

            SEO seo = new SEO
            {
                Url = request.Url,
                PageTitle = request.PageTitle,
                PageDescription = request.PageDescription,
                MetaKeyword = request.MetaKeyword,
                PublishedDate = request.PublishedDate,
                IsPublished = request.IsPublished,
                PublishType = PublishType.Public,
                IsFeatured = request.IsFeatured,
                FeaturedWeight = request.FeaturedWeight
            };

            Property property = new Property
            {
                Name = request.PropertyName,
                MasterProjectID = (request.MasterProjectID != 0) ? request.MasterProjectID : null,
                MasterPropertyID = null,
                ResidentialUnitID = null,
                ListingID = null,
                PropertyTypeID = request.PropertyTypeID,
                SubTypeID = null,
                GradeID = request.GradeID,
                Address = address,
                ContactID = (request.PropertyContactID != 0) ? request.PropertyContactID : null,
                OwnerID = (request.PropertyOwnerID != 0) ? request.PropertyOwnerID : null,
                OwnerCompanyID = (request.PropertyOwnerCompanyID != 0) ? request.PropertyOwnerCompanyID : null,
                Agents = agents,
                SEO = seo
            };

            Building building = new Building
            {
                Name = request.Name,
                Property = property,
                PEZA = pezaStatus,
                ACSystem = request.ACSystem,
                Amenities = request.Amenities,
                CeilingHeight = request.CeilingHeight,
                DateBuilt = request.DateBuilt,
                DensityRatio = request.DensityRatio,
                EfficiencyRatio = request.EfficiencyRatio,
                Elevators = request.Elevators,
                GrossBuildingSize = request.GrossBuildingSize,
                GrossLeasableSize = request.GrossLeasableSize,
                LEED = request.LEED,
                MinimumLeaseTerm = request.MinimumLeaseTerm,
                OperatingHours = request.OperatingHours,
                OwnershipTypeID = (request.OwnershipTypeID != 0) ? request.OwnershipTypeID : null,
                ParkingElevator = request.ParkingElevator,
                PassengerElevator = request.PassengerElevator,
                Power = request.Power,
                ProjectStatusID = (request.ProjectStatusID != 0) ? request.ProjectStatusID : null,
                TypicalFloorPlateSize = request.TypicalFloorPlateSize,
                ServiceElevator = request.ServiceElevator,
                Telcos = request.Telcos,
                TenantMix = request.TenantMix,
                TotalFloors = request.TotalFloors,
                TotalUnits = request.TotalUnits,
                TurnOverDate = request.TurnOverDate,
                WebPage = request.WebPage,
                YearBuilt = request.YearBuilt,
                LeasingContactID = (request.LeasingContactID != 0) ? request.LeasingContactID : null,
                PropertyManagementID = (request.PropertyManagementID != 0) ? request.PropertyManagementID : null,
                DeveloperID = (request.DeveloperID != 0) ? request.DeveloperID : null
            };

            try
            {
                _context.Buildings.Add(building);
                await _context.SaveChangesAsync(cancellationToken);

                if (request.Floors != null)
                {
                    if (request.Floors.Count() > 0)
                    {
                        foreach (var item in request.Floors)
                        {
                            Floor floor = new Floor
                            {
                                BuildingID = building.ID,
                                Name = item.Name,
                                FloorPlateSize = item.FloorPlateSize,
                                Sort = item.Sort
                            };
                            _context.Floors.Add(floor);
                            if (item.Units != null)
                            {
                                if (item.Units.Count() > 0)
                                {
                                    foreach (var un in item.Units)
                                    {
                                        Domain.Entities.Core.Unit unit = new Domain.Entities.Core.Unit
                                        {
                                            Name = un.Name,
                                            FloorID = floor.ID,
                                            ACCharges = un.ACCharges,
                                            ACExtensionCharges = un.ACExtensionCharges,
                                            BasePrice = un.BasePrice,
                                            CUSA = un.CUSA,
                                            EscalationRate = un.EscalationRate,
                                            HandOverDate = un.HandOverDate,
                                            HandOverConditionID = un.HandOverConditionID,
                                            LeaseFloorArea = un.LeaseFloorArea,
                                            ListingTypeID = un.ListingTypeID,
                                            MinimumLeaseTerm = un.MinimumLeaseTerm,
                                            ParkingRent = un.ParkingRent,
                                            UnitNumber = un.UnitNumber,
                                            UnitStatusID = un.UnitStatusID
                                        };
                                        _context.Units.Add(unit);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }

            return building.ID;
        }
    }
}
