using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;
using System;
using System.Linq;

namespace apollo.Application.Units.Queries.DTOs
{
    public class UnitShortDetailDTO : IMapFrom<Unit>
    {
        public int ID { get; set; }

        public int FloorID { get; set; }

        public string FloorName { get; set; }

        public int FloorSort { get; set; }

        public int? PropertyID { get; set; }

        public int UnitStatusID { get; set; }

        public int AvailabilityID { get; set; }

        public int ListingTypeID { get; set; }

        public int HandOverConditionID { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string UnitNumber { get; set; }

        public string UnitStatusName { get; set; }

        public string ListingTypeName { get; set; }

        public string AvailabilityName { get; set; }

        public decimal LeaseFloorArea { get; set; }

        public int MinimumLeaseTerm { get; set; }

        public string TenantClassification { get; set; }

        public string HandOverConditionName { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CUSA { get; set; }

        public string ACCharges { get; set; }

        public DateTime? EndDate { get; set; }

        public string ACExtensionCharges { get; set; }

        public decimal EscalationRate { get; set; }

        public decimal ParkingRent { get; set; }

        public DateTime? HandOverDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Unit, UnitShortDetailDTO>()
                .ForMember(i => i.FloorName, o => o.MapFrom(m => m.Floor.Name))
                .ForMember(i => i.FloorSort, o => o.MapFrom(m => m.Floor.Sort))
                .ForMember(i => i.ListingTypeName, o => o.MapFrom(m => m.ListingType.Name))
                .ForMember(i => i.UnitStatusName, o => o.MapFrom(m => m.UnitStatus.Name))
                .ForMember(i => i.TenantClassification, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).TenantClassification.Name))
                .ForMember(i => i.EndDate, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).EndDate))
                .ForMember(i => i.CompanyName, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).Company.Name));
        }
    }


    public class UnitDetailForOfferDTO : IMapFrom<Unit>
    {
        public int ID { get; set; }

        public int FloorID { get; set; }

        public string FloorName { get; set; }

        public string Name { get; set; }

        public string UnitNumber { get; set; }

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

        public decimal EscalationRate { get; set; }

        public decimal ParkingRent { get; set; }

        public DateTime? HandOverDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Unit, UnitDetailForOfferDTO>()
                .ForMember(i => i.FloorName, o => o.MapFrom(m => m.Floor.Name))
                .ForMember(i => i.ListingTypeName, o => o.MapFrom(m => m.ListingType.Name))
                .ForMember(i => i.UnitStatusName, o => o.MapFrom(m => m.UnitStatus.Name))
                .ForMember(i => i.AvailabilityName, o => o.MapFrom(m => m.Availability.Name));
        }
    }
}
