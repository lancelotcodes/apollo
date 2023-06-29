using apollo.Domain.Entities.References;
using apollo.Domain.Enums;
using Shared.Domain.Common;
using System.Collections.ObjectModel;

namespace apollo.Domain.Entities.Core
{
    public class OfferOption : AuditableEntity, BaseEntityId
    {
        public int ID { get; set; }
        public int? ContactID { get; set; }
        public Contact Contact { get; set; }
        public int AgentID { get; set; }
        public Contact Agent { get; set; }
        public int? CompanyID { get; set; }
        public Company Company { get; set; }
        public string UserID { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public ApplicationUser User { get; set; }
        public int? PropertyTypeID { get; set; }
        public PropertyType PropertyType { get; set; }
        public int? ListingTypeID { get; set; }
        public ListingType ListingType { get; set; }
        public PEZAStatus? PEZA { get; set; }
        public bool OperatingHours { get; set; }
        public int? HandOverConditionID { get; set; }
        public HandOverCondition HandOverCondition { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public string Cities { get; set; }
        public string Provinces { get; set; }
        public string SubMarkets { get; set; }
        public Collection<OfferUnit> OfferUnits { get; set; }
    }
}
