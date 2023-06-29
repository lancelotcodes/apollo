using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;
using System;

namespace apollo.Application.Contracts.Queries.DTOs
{
    public class ContractListDTO : IMapFrom<Contract>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TenantClassificationID { get; set; }
        public string TenantClassificationName { get; set; }
        public decimal EstimatedArea { get; set; }
        public int LeaseTerm { get; set; }
        public int BrokerID { get; set; }
        public string BrokerName { get; set; }
        public string BrokerPhone { get; set; }
        public string BrokerEmail { get; set; }
        public int BrokerCompanyID { get; set; }
        public string BrokerCompanyName { get; set; }
        public bool IsHistorical { get; set; }
        public decimal ClosingRate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contract, ContractListDTO>()
                .ForMember(i => i.CompanyName, o => o.MapFrom(m => m.Company.Name))
                .ForMember(i => i.ContactName, o => o.MapFrom(m => m.Contact.FirstName + " " + m.Contact.LastName))
                .ForMember(i => i.ContactPhone, o => o.MapFrom(m => m.Contact.PhoneNumber))
                .ForMember(i => i.ContactEmail, o => o.MapFrom(m => m.Contact.Email))
                .ForMember(i => i.BrokerName, o => o.MapFrom(m => m.Broker.FirstName + " " + m.Broker.LastName))
                .ForMember(i => i.BrokerPhone, o => o.MapFrom(m => m.Broker.PhoneNumber))
                .ForMember(i => i.BrokerEmail, o => o.MapFrom(m => m.Broker.Email))
                .ForMember(i => i.BrokerCompanyName, o => o.MapFrom(m => m.BrokerCompany.Name))
                .ForMember(i => i.TenantClassificationName, o => o.MapFrom(m => m.TenantClassification.Name));
        }
    }
}
