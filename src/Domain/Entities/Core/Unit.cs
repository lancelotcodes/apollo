using apollo.Domain.Entities.References;
using Shared.Domain.Common;
using System;

namespace apollo.Domain.Entities.Core
{
    public class Unit : AuditableEntity, BaseEntityId
    {
        public int ID { get; set; }

        public int FloorID { get; set; }

        public virtual Floor Floor { get; set; }

        public int? PropertyID { get; set; }

        public virtual Property Property { get; set; }

        public string UnitNumber { get; set; }

        public string Name { get; set; }

        public int UnitStatusID { get; set; }

        public virtual UnitStatus UnitStatus { get; set; }

        public int AvailabilityID { get; set; }

        public virtual Availability Availability { get; set; }

        public decimal LeaseFloorArea { get; set; }

        public int ListingTypeID { get; set; }

        public virtual ListingType ListingType { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CUSA { get; set; }

        public int HandOverConditionID { get; set; }

        public virtual HandOverCondition HandOverCondition { get; set; }

        public string ACCharges { get; set; }

        public string ACExtensionCharges { get; set; }

        public decimal EscalationRate { get; set; }

        public int MinimumLeaseTerm { get; set; }

        public decimal ParkingRent { get; set; }

        public DateTime? HandOverDate { get; set; }

        public string Notes { get; set; }
    }
}