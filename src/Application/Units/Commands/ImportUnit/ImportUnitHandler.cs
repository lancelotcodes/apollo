using apollo.Application.Units.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.References;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using Shared.Constants;
using Shared.Contracts;
using Shared.Extensions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace apollo.Application.Units.Commands.CreateUnit
{
    public class ImportUnitHandler : IRequestHandler<ImportUnitRequest, ImportStackingPlanDetailDTO>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ImportUnitHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ImportStackingPlanDetailDTO> Handle(ImportUnitRequest request, CancellationToken cancellationToken)
        {
            int total = 0;
            int savedCount = 0;
            int failedCount = 0;
            if (request.Units != null && request.Units.Count > 0)
            {
                request.Units = request.Units.OrderBy(u => u.FloorName, new SemiNumericComparer()).OrderBy(x => (x.FloorName.ToLower().Contains("ph") || x.FloorName.ToLower().Contains("penthouse")) ? 1 : 2).ToList();
                total = request.Units.Count;
                foreach (var item in request.Units)
                {
                    var createCommand = new SaveUnitRequest
                    {
                        ID = item.ID.HasValue ? item.ID.Value : 0,
                        Name = item.Name,
                        UnitNumber = item.UnitNumber,
                        LeaseFloorArea = item.LeaseFloorArea,
                        BasePrice = item.BasePrice,
                        CUSA = item.CUSA,
                        ACCharges = item.ACCharges,
                        ACExtensionCharges = item.ACExtensionCharges,
                        EscalationRate = item.EscalationRate,
                        MinimumLeaseTerm = item.MinimumLeaseTerm,
                        ParkingRent = item.ParkingRent,
                        HandOverDate = item.HandOverDate.ToValidDate(),
                        Notes = item.Notes,
                    };

                    if (!string.IsNullOrWhiteSpace(item.BuildingName))
                    {
                        var building = await _repository.UntrackFirstAsync<Building>(x => x.Name.ToLower() == item.BuildingName.ToLower(), i => i.Include(i => i.Property).ThenInclude(x => x.PropertyType));
                        if (building != null)
                        {
                            Domain.Entities.Core.Unit unit = new Domain.Entities.Core.Unit();
                            if (createCommand.ID > 0)
                            {
                                unit = await _repository.FirstAsync<Domain.Entities.Core.Unit>(x => x.ID == createCommand.ID, i => i.Include(x => x.Property).ThenInclude(x => x.Agents));
                                if (unit == null || unit.Property == null)
                                {
                                    failedCount++;
                                    continue;
                                }

                                createCommand.PropertyID = unit.PropertyID;
                                createCommand.AvailabilityID = unit.AvailabilityID;
                            }
                            else
                            {
                                createCommand.AvailabilityID = 1;
                            }

                            _mapper.Map(createCommand, unit);

                            var floorName = string.Empty;

                            var floor = await _repository.UntrackFirstAsync<Floor>(x => x.Name == item.FloorName && x.BuildingID == building.ID);
                            if (floor != null)
                            {
                                unit.FloorID = floor.ID;
                                floorName = floor.Name;
                            }
                            else
                            {
                                int sort = 1;
                                var floors = _repository.Fetch<Floor>(x => x.BuildingID == building.ID);

                                if (floors != null && floors.Count() > 0)
                                {
                                    sort = floors.OrderByDescending(x => x.Sort).FirstOrDefault().Sort + 1;
                                }

                                var createFloor = new Floor()
                                {
                                    BuildingID = building.ID,
                                    Name = item.FloorName,
                                    Sort = sort,
                                    FloorPlateSize = item.LeaseFloorArea,
                                    IsDeleted = false,
                                    IsActive = true,
                                };
                                unit.Floor = createFloor;
                                floorName = createFloor.Name;
                            }

                            if (unit.ID == 0)
                            {
                                ApplicationMapping.PropertyMappings.TryGetValue(building.Property.PropertyType.Name, out var propertyTypeName);

                                if (string.IsNullOrEmpty(propertyTypeName))
                                {
                                    failedCount++;
                                    continue;
                                }

                                var unitPropertyType = await _repository.UntrackFirstAsync<PropertyType>(x => x.Name == propertyTypeName);
                                var property = new Property
                                {
                                    Name = $"{building.Property.Name} Unit-{floorName}",
                                    PropertyTypeID = unitPropertyType.ID,
                                    GradeID = building.Property.GradeID,
                                    OwnerID = building.Property.OwnerID,
                                    IsExclusive = building.Property.IsExclusive,
                                    OwnerCompanyID = building.Property.OwnerCompanyID,
                                    Note = building.Property.Note,
                                };

                                unit.Property = property;
                            }

                            Company agentCompany = null;

                            if (!string.IsNullOrWhiteSpace(item.CompanyName))
                            {
                                agentCompany = _repository.UntrackFirst<Company>(x => x.Name == item.CompanyName);
                                if (agentCompany == null)
                                {
                                    agentCompany = new Company()
                                    {
                                        Name = item.CompanyName
                                    };
                                }
                            }

                            if (!string.IsNullOrEmpty(item.ContactName))
                            {
                                var name = item.ContactName.Split(" ");
                                if (name.Length > 1)
                                {
                                    var contact = _repository.First<Contact>(x => x.FirstName.ToLower() == name[0].ToLower() && x.LastName.ToLower() == name[1]);

                                    if (contact != null)
                                    {
                                        if (agentCompany != null && contact.Company == null)
                                        {
                                            if (agentCompany.ID > 0)
                                            {
                                                contact.CompanyID = agentCompany.ID;
                                            }
                                            else
                                            {
                                                contact.Company = agentCompany;
                                            }
                                        }

                                        var agent = new PropertyAgent { AgentID = contact.ID, AgentType = AgentType.Main };

                                        if (unit.Property.Agents == null || !unit.Property.Agents.Any())
                                        {
                                            unit.Property.Agents = new List<PropertyAgent>
                                            {
                                                agent
                                            };
                                        }
                                        else
                                        {
                                            if (unit.Property.Agents.FirstOrDefault(x => x.AgentID == contact.ID) == null)
                                            {
                                                unit.Property.Agents.ToList().ForEach(x => x.AgentType = AgentType.Secondary);
                                                unit.Property.Agents.Add(agent);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        contact = new Contact()
                                        {
                                            FirstName = name[0],
                                            LastName = name[1],
                                            Email = item.ContactEmail,
                                            PhoneNumber = item.ContactPhoneNumber
                                        };

                                        if (agentCompany != null && contact.Company == null)
                                        {
                                            if (agentCompany.ID > 0)
                                            {
                                                contact.CompanyID = agentCompany.ID;
                                            }
                                            else
                                            {
                                                contact.Company = agentCompany;
                                            }
                                        }

                                        if (unit.Property.Agents == null)
                                        {
                                            unit.Property.Agents = new List<PropertyAgent>();
                                        }
                                        unit.Property.Agents.ToList().ForEach(x => x.AgentType = AgentType.Secondary);
                                        unit.Property.Agents.Add(new PropertyAgent { Agent = contact, AgentType = AgentType.Main });
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(item.TenantName) || !string.IsNullOrEmpty(item.BrokerName))
                            {
                                var contract = new Contract()
                                {
                                    ClosingRate = item.ClosingRate ?? 0,
                                    EstimatedArea = item.EstimatedArea ?? 0,
                                    LeaseTerm = item.LeaseTerm ?? 0,
                                    EndDate = item.EndDate.ToValidDate(),
                                    StartDate = item.StartDate.ToValidDate(),
                                    IsHistorical = item.IsHistorical ?? true,
                                };

                                if (!string.IsNullOrWhiteSpace(item.TenantName))
                                {
                                    var tenant = _repository.UntrackFirst<Company>(x => x.Name == item.TenantName);
                                    if (tenant != null)
                                    {
                                        contract.CompanyID = tenant.ID;
                                    }
                                    else
                                    {
                                        tenant = new Company()
                                        {
                                            Name = item.TenantName
                                        };
                                        contract.Company = tenant;
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(item.BrokerName))
                                {
                                    var brokerName = item.BrokerName.Split(" ");
                                    if (brokerName.Length > 1)
                                    {
                                        var broker = _repository.UntrackFirst<Contact>(x => x.FirstName.ToLower() == brokerName[0].ToLower() && x.LastName.ToLower() == brokerName[1]);
                                        if (broker != null)
                                        {
                                            contract.BrokerID = broker.ID;
                                        }
                                        else
                                        {
                                            broker = new Contact()
                                            {
                                                FirstName = brokerName[0],
                                                LastName = brokerName[1],
                                            };
                                            contract.Broker = broker;
                                        }
                                    }
                                }

                                var clasfication = _repository.UntrackFirst<TenantClassification>(x => x.Name == item.TenantClassification);
                                contract.TenantClassificationID = clasfication != null ? clasfication.ID : 1;
                                unit.Property.Contracts.Add(contract);
                            }

                            var unitStatus = await _repository.UntrackFirstAsync<UnitStatus>(x => x.Name == item.UnitStatusName);
                            unit.UnitStatusID = unitStatus != null ? unitStatus.ID : 1;

                            var listingType = await _repository.UntrackFirstAsync<ListingType>(x => x.Name == item.ListingTypeName);
                            unit.ListingTypeID = listingType != null ? listingType.ID : 1;

                            var handOverCondition = await _repository.UntrackFirstAsync<HandOverCondition>(x => x.Name == item.HandOverConditionName);
                            unit.HandOverConditionID = handOverCondition != null ? handOverCondition.ID : 1;

                            try
                            {
                                _repository.Save(unit);
                                await _repository.SaveChangesAsync();
                                savedCount++;
                            }
                            catch (Exception ex)
                            {
                                failedCount++;
                            }
                        }
                        else
                        {
                            failedCount++;
                        }
                    }
                    else
                    {
                        failedCount++;
                    }
                }
            }

            return new ImportStackingPlanDetailDTO
            {
                Total = total,
                FailedCount = failedCount,
                ImportedCount = savedCount
            };
        }
    }
}