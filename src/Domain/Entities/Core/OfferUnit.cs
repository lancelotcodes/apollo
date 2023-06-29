using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class OfferUnit : BaseEntityId
    {
        public int ID { get; set; }
        public int OfferId { get; set; }
        public OfferOption OfferOption { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
