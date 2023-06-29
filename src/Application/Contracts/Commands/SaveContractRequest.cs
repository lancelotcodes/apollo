using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using System;

namespace apollo.Application.Contracts.Commands
{
    public class SaveContractRequest : IRequest<SaveEntityResult>, IMapFrom<Contract>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyID { get; set; }
        public int CompanyID { get; set; }
        public int ContactID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TenantClassificationID { get; set; }
        public decimal EstimatedArea { get; set; }
        public int LeaseTerm { get; set; }
        public int BrokerID { get; set; }
        public int BrokerCompanyID { get; set; }
        public bool IsHistorical { get; set; }
        public decimal ClosingRate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SaveContractRequest, Contract>(MemberList.Source);
        }
    }
}
