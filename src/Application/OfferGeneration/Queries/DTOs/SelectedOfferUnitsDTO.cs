using apollo.Application.Agents.Queries.DTOs;
using apollo.Application.Common.Mappings;
using apollo.Application.PropertyAddress.Queries.DTOs;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apollo.Application.OfferGeneration.Queries.DTOs
{
    public class SelectedOfferUnitsDTO
    {
        public string ContactName { get; set; }
        public IEnumerable<UnitDetailForOfferExportDTO> Units { get; set; }
        public ContactDTO Agent { get; set; }
    }


    public class UnitDetailForOfferExportDTO : IMapFrom<Unit>
    {
        public int ID { get; set; }

        public int FloorID { get; set; }

        public string FloorName { get; set; }

        public string Name { get; set; }

        public string BuildingName { get; set; }

        public string GradeName { get; set; }

        public string DeveloperName { get; set; }

        public int YearBuilt { get; set; }

        public string UnitNumber { get; set; }

        public string PEZAStatusName { get; set; }

        public double GrossBuildingSize { get; set; }

        public string TypicalFloorPlateSize { get; set; }

        public decimal EfficiencyRatio { get; set; }

        public string CeilingHeight { get; set; }

        public string Power { get; set; }

        public string ACSystem { get; set; }

        public double DensityRatio { get; set; }

        public int TotalFloors { get; set; }

        public string Elevators { get; set; }

        public string Telcos { get; set; }

        public string Amenities { get; set; }

        public string UnitStatusName { get; set; }

        public string ListingTypeName { get; set; }

        public string AvailabilityName { get; set; }

        public decimal LeaseFloorArea { get; set; }

        public int MinimumLeaseTerm { get; set; }

        public string HandOverConditionName { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CUSA { get; set; }

        public string ACCharges { get; set; }

        public string ACExtensionCharges { get; set; }

        public string MainImage { get; set; }

        public string FloorPlanImage { get; set; }

        public decimal EscalationRate { get; set; }

        public decimal ParkingRent { get; set; }

        public DateTime? HandOverDate { get; set; }

        public AddressShortDTO Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Unit, UnitDetailForOfferExportDTO>()
                .ForMember(i => i.FloorName, o => o.MapFrom(m => m.Floor.Name))
                .ForMember(i => i.ListingTypeName, o => o.MapFrom(m => m.ListingType.Name))
                .ForMember(i => i.UnitStatusName, o => o.MapFrom(m => m.UnitStatus.Name))
                .ForMember(i => i.GradeName, o => o.MapFrom(m => m.Floor.Building.Property.Grade.Name))
                .ForMember(i => i.AvailabilityName, o => o.MapFrom(m => m.Availability.Name))
                .ForMember(i => i.BuildingName, o => o.MapFrom(m => m.Floor.Building.Name))
                .ForMember(i => i.DeveloperName, o => o.MapFrom(m => m.Floor.Building.Developer.Name))
                .ForMember(i => i.YearBuilt, o => o.MapFrom(m => m.Floor.Building.YearBuilt))
                .ForMember(i => i.PEZAStatusName, o => o.MapFrom(m => m.Floor.Building.PEZA.ToString()))
                .ForMember(i => i.GrossBuildingSize, o => o.MapFrom(m => m.Floor.Building.GrossBuildingSize))
                .ForMember(i => i.TypicalFloorPlateSize, o => o.MapFrom(m => m.Floor.Building.TypicalFloorPlateSize))
                .ForMember(i => i.EfficiencyRatio, o => o.MapFrom(m => m.Floor.Building.EfficiencyRatio))
                .ForMember(i => i.CeilingHeight, o => o.MapFrom(m => m.Floor.Building.CeilingHeight))
                .ForMember(i => i.Power, o => o.MapFrom(m => m.Floor.Building.Power))
                .ForMember(i => i.ACSystem, o => o.MapFrom(m => m.Floor.Building.ACSystem))
                .ForMember(i => i.Telcos, o => o.MapFrom(m => m.Floor.Building.Telcos))
                .ForMember(i => i.TotalFloors, o => o.MapFrom(m => m.Floor.Building.TotalFloors))
                .ForMember(i => i.Elevators, o => o.MapFrom(m => m.Floor.Building.Elevators))
                .ForMember(i => i.DensityRatio, o => o.MapFrom(m => m.Floor.Building.DensityRatio))
                .ForMember(i => i.Amenities, o => o.MapFrom(m => m.Floor.Building.Amenities))
                .ForMember(i => i.Address, o => o.MapFrom(m => m.Floor.Building.Property.Address))
                .ForMember(i => i.MainImage, o => o.MapFrom(m => m.Floor.Building.Property.PropertyDocuments.FirstOrDefault(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage).Document.DocumentPath))
                .ForMember(i => i.FloorPlanImage, o => o.MapFrom(m => m.Floor.Building.Property.PropertyDocuments.FirstOrDefault(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.FloorPlanImage).Document.DocumentPath));
        }
    }
}
