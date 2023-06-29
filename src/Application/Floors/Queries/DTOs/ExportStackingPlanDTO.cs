using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using CsvHelper.Configuration;
using System;
using System.Linq;

namespace apollo.Application.Floors.Queries.DTOs
{
    public class ExportStackingPlanDTO : IMapFrom<Unit>
    {
        public int? ID { get; set; }

        public int? BuildingID { get; set; }

        public string Name { get; set; }

        public string BuildingName { get; set; }

        public string UnitStatusName { get; set; }

        public string UnitNumber { get; set; }

        public string FloorName { get; set; }

        public decimal LeaseFloorArea { get; set; }

        public string ListingTypeName { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CUSA { get; set; }

        public string HandOverConditionName { get; set; }

        public string ACCharges { get; set; }

        public string ACExtensionCharges { get; set; }

        public decimal EscalationRate { get; set; }

        public int MinimumLeaseTerm { get; set; }

        public decimal ParkingRent { get; set; }

        public DateTime? HandOverDate { get; set; }

        public string Notes { get; set; }

        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string CompanyName { get; set; }

        public string BrokerName { get; set; }

        public string TenantName { get; set; }

        public decimal? ClosingRate { get; set; }

        public string TenantClassification { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? EstimatedArea { get; set; }

        public int? LeaseTerm { get; set; }

        public bool? IsHistorical { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Unit, ExportStackingPlanDTO>()
                .ForMember(i => i.UnitStatusName, o => o.MapFrom(m => m.UnitStatus.Name))
                .ForMember(i => i.ListingTypeName, o => o.MapFrom(m => m.ListingType.Name))
                .ForMember(i => i.BuildingName, o => o.MapFrom(m => m.Floor.Building.Name))
                .ForMember(i => i.BuildingID, o => o.MapFrom(m => m.Floor.Building.ID))
                .ForMember(i => i.FloorName, o => o.MapFrom(m => m.Floor.Name))
            .ForMember(i => i.TenantName, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).Company.Name))
            .ForMember(i => i.TenantClassification, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).TenantClassification.Name))
            .ForMember(i => i.StartDate, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).StartDate))
            .ForMember(i => i.EndDate, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).EndDate))
            .ForMember(i => i.ClosingRate, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).ClosingRate))
            .ForMember(i => i.ContactName, o => o.MapFrom(m => $"{m.Property.Agents.FirstOrDefault(x => (!x.IsDeleted && x.AgentType == AgentType.Main) || !x.IsDeleted).Agent.FirstName} {m.Property.Agents.FirstOrDefault(x => (!x.IsDeleted && x.AgentType == AgentType.Main) || !x.IsDeleted).Agent.LastName}"))
            .ForMember(i => i.ContactEmail, o => o.MapFrom(m => m.Property.Agents.FirstOrDefault(x => (!x.IsDeleted && x.AgentType == AgentType.Main) || !x.IsDeleted).Agent.Email))
            .ForMember(i => i.ContactPhoneNumber, o => o.MapFrom(m => m.Property.Agents.FirstOrDefault(x => (!x.IsDeleted && x.AgentType == AgentType.Main) || !x.IsDeleted).Agent.PhoneNumber))
            .ForMember(i => i.CompanyName, o => o.MapFrom(m => m.Property.Agents.FirstOrDefault(x => (!x.IsDeleted && x.AgentType == AgentType.Main) || !x.IsDeleted).Agent.Company.Name))
            .ForMember(i => i.BrokerName, o => o.MapFrom(m => $"{m.Property.Contracts.OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).Broker.FirstName} {m.Property.Contracts.OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).Broker.LastName}"))
            .ForMember(i => i.EstimatedArea, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).EstimatedArea))
            .ForMember(i => i.IsHistorical, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).IsHistorical))
            .ForMember(i => i.LeaseTerm, o => o.MapFrom(m => m.Property.Contracts
                .OrderByDescending(c => c.EndDate).ThenByDescending(c => c.ID).FirstOrDefault(x => !x.IsHistorical).LeaseTerm));
        }
    }

    public sealed class ExportStackingPlanDTOMap : ClassMap<ExportStackingPlanDTO>
    {
        public ExportStackingPlanDTOMap()
        {
            Map(m => m.ID).Name("Unit ID");
            Map(m => m.Name).Name("Unit Name");
            Map(m => m.BuildingID).Name("Building ID");
            Map(m => m.BuildingName).Name("Building Name");
            Map(m => m.UnitStatusName).Name("Status");
            Map(m => m.UnitNumber).Name("Unit Number");
            Map(m => m.FloorName).Name("Floor Location");
            Map(m => m.LeaseFloorArea).Name("Leasable Floor Area \n (in SQM)");
            Map(m => m.ListingTypeName).Name("Listing Type");
            Map(m => m.BasePrice).Name("Base Price \n per SQM (PHP)");
            Map(m => m.CUSA).Name("CUSA per SQM (PHP)");
            Map(m => m.HandOverConditionName).Name("Handover Condition");
            Map(m => m.ACCharges).Name("AC Charges (PHP)");
            Map(m => m.ACExtensionCharges).Name("AC Extension Charges\n (PHP)");
            Map(m => m.EscalationRate).Name("Escalation Rate");
            Map(m => m.MinimumLeaseTerm).Name("Minimum Lease Term");
            Map(m => m.ParkingRent).Name("Parking Rent per slot \n (PHP)");
            Map(m => m.HandOverDate).Name("Handover Date");
            Map(m => m.Notes).Name("Notes");
            Map(m => m.ContactName).Name("Contact Person");
            Map(m => m.ContactPhoneNumber).Name("Contact Phone");
            Map(m => m.ContactEmail).Name("Contact Email");
            Map(m => m.CompanyName).Name("Company Name");
            Map(m => m.BrokerName).Name("Broker Name");
            Map(m => m.TenantName).Name("Tenant Name");
            Map(m => m.StartDate).Name("Tenant Start Date");
            Map(m => m.EndDate).Name("Tenant End Date");
            Map(m => m.ClosingRate).Name("Closing Rate");
            Map(m => m.TenantClassification).Name("Tenant Classification");
            Map(m => m.LeaseTerm).Name("Lease Term");
            Map(m => m.EstimatedArea).Name("Estimated Area");
            Map(m => m.IsHistorical).Name("Is Historical?");
        }
    }
}
