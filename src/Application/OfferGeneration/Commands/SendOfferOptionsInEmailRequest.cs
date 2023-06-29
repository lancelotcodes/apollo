using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace apollo.Application.Agents.Commands.SavePropertyAgent
{
    public class SendOfferOptionsInEmailRequest : IRequest<bool>, IMapFrom<OfferOption>
    {
        public int ContactID { get; set; }
        public int? CompanyID { get; set; }
        public int AgentID { get; set; }
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string Message { get; set; }
        public int? PropertyTypeID { get; set; }
        public int? ListingTypeID { get; set; }
        public PEZAStatus? PEZA { get; set; }
        public bool OperatingHours { get; set; }
        public int? HandOverConditionID { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public List<int> CityIds { get; set; }
        public List<int> ProvinceIds { get; set; }
        public List<int> SubMarketIds { get; set; }
        public List<int> UnitIds { get; set; }
        public TemplateType TemplateType { get; set; } = TemplateType.Powerpoint;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SendOfferOptionsInEmailRequest, OfferOption>(MemberList.Source);
        }
    }

    public class ExportOfferOptionsRequest : IRequest<bool>, IMapFrom<OfferOption>
    {
        public int ContactID { get; set; }
        public int? CompanyID { get; set; }
        public int AgentID { get; set; }
        public int? PropertyTypeID { get; set; }
        public int? ListingTypeID { get; set; }
        public PEZAStatus? PEZA { get; set; }
        public bool OperatingHours { get; set; }
        public int? HandOverConditionID { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public List<int> CityIds { get; set; }
        public List<int> ProvinceIds { get; set; }
        public List<int> SubMarketIds { get; set; }
        public List<int> UnitIds { get; set; }
        public TemplateType TemplateType { get; set; } = TemplateType.Powerpoint;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExportOfferOptionsRequest, OfferOption>(MemberList.Source);
        }
    }
}
