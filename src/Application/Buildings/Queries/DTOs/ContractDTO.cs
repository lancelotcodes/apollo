using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;
using System;

namespace apollo.Application.Buildings.Queries.DTOs
{
    public class ContractDTO : IMapFrom<Contract>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public int? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int? ContactID { get; set; }
        public string ContactName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TenantClassificationID { get; set; }
        public string TenantClassificationName { get; set; }
        public decimal EstimatedArea { get; set; }
        public int LeaseTerm { get; set; }
        public decimal ClosingRate { get; set; }
        public int? BrokerID { get; set; }
        public string BrokerName { get; set; }
        public int? BrokerCompanyID { get; set; }
        public string BrokerCompanyName { get; set; }
        public bool IsHistorical { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contract, ContractDTO>()
                .ForMember(i => i.PropertyID, o => o.MapFrom(m => m.PropertyID))
                .ForMember(i => i.PropertyName, o => o.MapFrom(m => m.Property.Name))
                .ForMember(i => i.CompanyID, o => o.MapFrom(m => m.CompanyID))
                .ForMember(i => i.CompanyName, o => o.MapFrom(m => m.Company.Name))
                .ForMember(i => i.ContactID, o => o.MapFrom(m => m.ContactID))
                .ForMember(i => i.ContactName, o => o.MapFrom(m => m.Contact.FirstName + " " + m.Contact.LastName))
                .ForMember(i => i.BrokerID, o => o.MapFrom(m => m.BrokerID))
                .ForMember(i => i.BrokerName, o => o.MapFrom(m => m.Broker.FirstName + " " + m.Broker.LastName))
                .ForMember(i => i.BrokerCompanyID, o => o.MapFrom(m => m.BrokerCompanyID))
                .ForMember(i => i.BrokerCompanyName, o => o.MapFrom(m => m.BrokerCompany.Name))
                .ForMember(i => i.TenantClassificationID, o => o.MapFrom(m => m.TenantClassificationID))
                .ForMember(i => i.TenantClassificationName, o => o.MapFrom(m => m.TenantClassification.Name));
        }
    }
}
