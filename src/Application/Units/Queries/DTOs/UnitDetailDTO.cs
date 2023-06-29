using apollo.Application.Agents.Queries.DTOs;
using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apollo.Application.Units.Queries.DTOs
{
    public class UnitDetailDTO : IMapFrom<Unit>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int FloorID { get; set; }

        public string UnitNumber { get; set; }

        public int UnitStatusID { get; set; }

        public string UnitStatusName { get; set; }

        public decimal LeaseFloorArea { get; set; }

        public int ListingTypeID { get; set; }

        public string ListingTypeName { get; set; }

        public string TenantClassification { get; set; }

        public DateTime? StartDate { get; set; } // Commencement Date

        public DateTime? EndDate { get; set; } // Lease Expiry Date

        public int HandOverConditionID { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CUSA { get; set; }

        public string HandOverConditionName { get; set; }

        public string ACCharges { get; set; }

        public string ACExtensionCharges { get; set; }

        public decimal EscalationRate { get; set; }

        public int MinimumLeaseTerm { get; set; }

        public decimal ParkingRent { get; set; }

        public DateTime? HandOverDate { get; set; }

        public int? PropertyID { get; set; }

        public int AvailabilityID { get; set; }

        public string AvailabilityName { get; set; }

        public string UnitMainImage { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<PropertyAgentDTO> Agents { get; set; }

        public ContractDTO Contract { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Unit, UnitDetailDTO>()
                .ForMember(i => i.UnitStatusName, o => o.MapFrom(m => m.UnitStatus.Name))
                .ForMember(i => i.Agents, o => o.MapFrom(m => m.Property.Agents))
                .ForMember(i => i.AvailabilityName, o => o.MapFrom(m => m.Availability.Name))
                .ForMember(i => i.ListingTypeName, o => o.MapFrom(m => m.ListingType.Name))
                .ForMember(i => i.TenantClassification, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).TenantClassification.Name))
                .ForMember(i => i.StartDate, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).StartDate))
                .ForMember(i => i.EndDate, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).EndDate))
                .ForMember(i => i.Contract, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical)))
                .ForMember(i => i.UnitMainImage, o => o.MapFrom(m => m.Property.PropertyDocuments.FirstOrDefault(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage).Document.DocumentPath))
                .ForMember(i => i.Images, o => o.MapFrom(m => m.Property.PropertyDocuments.Where(x => x.IsDeleted == false
                                        && (x.DocumentType == PropertyDocumentType.MainImage || x.DocumentType == PropertyDocumentType.FloorPlanImage)).Select(x => x.Document.DocumentPath)));
        }
    }
}
