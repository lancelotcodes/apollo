using apollo.Domain.Entities.References;
using Shared.Domain.Common;
using System;

namespace apollo.Domain.Entities.Core
{
    public class ResidentialListing : AuditableEntity, BaseEntityId
    {
        public ResidentialListing()
        {
        }

        public int ID { get; set; }

        #region Public Properties

        public string Name { get; set; }

        public int PropertyID { get; set; }

        public virtual Property Property { get; set; }

        public int ListingTypeID { get; set; }

        public virtual ListingType ListingType { get; set; }

        public decimal FloorArea { get; set; }

        public decimal LotArea { get; set; }

        public decimal SalePrice { get; set; }

        public decimal RentPrice { get; set; }

        public int Bathroom { get; set; }

        public int Bedroom { get; set; }

        public int ParkingSlot { get; set; }

        public string HandOverCondition { get; set; }

        public DateTime? HandOverDate { get; set; }

        public int AgentID { get; set; }
        public virtual Contact Agent { get; set; }

        #endregion

        #region Private Properties


        #endregion
    }
}
