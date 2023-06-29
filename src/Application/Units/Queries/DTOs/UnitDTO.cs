﻿using apollo.Application.Agents.Queries.DTOs;
using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apollo.Application.Units.Queries.DTOs
{
    public class UnitDTO : IMapFrom<Unit>
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

        public IEnumerable<ContractDTO> Contracts { get; set; }

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

        public string CompanyName { get; set; }

        public IEnumerable<PropertyAgentDTO> Agents { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Unit, UnitDTO>()
                .ForMember(i => i.UnitStatusName, o => o.MapFrom(m => m.UnitStatus.Name))
                .ForMember(i => i.Agents, o => o.MapFrom(m => m.Property.Agents))
                .ForMember(i => i.ListingTypeName, o => o.MapFrom(m => m.ListingType.Name))
                .ForMember(i => i.TenantClassification, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).TenantClassification.Name))
                .ForMember(i => i.StartDate, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).StartDate))
                .ForMember(i => i.EndDate, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).EndDate))
                .ForMember(i => i.CompanyName, o => o.MapFrom(m => m.Property.Contracts
                    .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).Company.Name));
        }
    }
}
