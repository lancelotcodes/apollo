using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using System;

namespace apollo.Application.Buildings.Queries.DTOs
{
    public class PropertyBuildingDTO : IMapFrom<Building>
    {
        public int ID { get; set; }

        public int PropertyID { get; set; }

        public string Name { get; set; }

        public DateTime? DateBuilt { get; set; }

        public int YearBuilt { get; set; }

        public PEZAStatus? PEZA { get; set; }

        public string PEZAName { get; set; }

        public bool OperatingHours { get; set; }

        public string LEED { get; set; }

        public DateTime? TurnOverDate { get; set; }

        public string TenantMix { get; set; }

        public double GrossBuildingSize { get; set; }

        public string GrossLeasableSize { get; set; }

        public string TypicalFloorPlateSize { get; set; }

        public int TotalFloors { get; set; }

        public int TotalUnits { get; set; }

        public decimal EfficiencyRatio { get; set; }

        public string CeilingHeight { get; set; }

        public string MinimumLeaseTerm { get; set; }

        public string Elevators { get; set; }

        public string Power { get; set; }

        public string ACSystem { get; set; }

        public string Telcos { get; set; }

        public string Amenities { get; set; }

        public string WebPage { get; set; }

        public double DensityRatio { get; set; }

        public string ParkingElevator { get; set; }

        public string PassengerElevator { get; set; }

        public string ServiceElevator { get; set; }

        public int? LeasingContactID { get; set; }

        public string LeasingContactName { get; set; }

        public int? DeveloperID { get; set; }

        public string DeveloperName { get; set; }

        public int? PropertyManagementID { get; set; }

        public string PropertyManagementName { get; set; }

        public int? OwnershipTypeID { get; set; }

        public string OwnershipTypeName { get; set; }

        public int? ProjectStatusID { get; set; }

        public string ProjectStatusName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Building, PropertyBuildingDTO>()
                .ForMember(i => i.PropertyManagementName, o => o.MapFrom(m => m.PropertyManagement.Name))
                .ForMember(i => i.DeveloperName, o => o.MapFrom(m => m.Developer.Name))
                .ForMember(i => i.LeasingContactName, o => o.MapFrom(m => m.LeasingContact.Name))
                .ForMember(i => i.OwnershipTypeName, o => o.MapFrom(m => m.OwnershipType.Name))
                .ForMember(i => i.ProjectStatusName, o => o.MapFrom(m => m.ProjectStatus.Name))
                .ForMember(i => i.PEZAName, o => o.MapFrom(x => x.PEZA.ToString()));
        }
    }

    public class BuildingShortDTO : IMapFrom<Building>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? DateBuilt { get; set; }

        public PEZAStatus? PEZA { get; set; }

        public string PEZAName { get; set; }

        public bool OperatingHours { get; set; }

        public string LEED { get; set; }

        public DateTime? TurnOverDate { get; set; }

        public double GrossBuildingSize { get; set; }

        public string GrossLeasableSize { get; set; }

        public string TypicalFloorPlateSize { get; set; }

        public string MinimumLeaseTerm { get; set; }

        public double DensityRatio { get; set; }

        public decimal EfficiencyRatio { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Building, BuildingShortDTO>()
                 .ForMember(i => i.PEZAName, o => o.MapFrom(x => x.PEZA.ToString()));
        }
    }
}
