using System;

namespace apollo.Application.Units.Commands.MigrateUnit
{
    public class MigrateUnitDTO
    {
        public int FloorID { get; set; }

        public int? PropertyID { get; set; }

        public string UnitNumber { get; set; }

        public string Name { get; set; }

        public int UnitStatusID { get; set; }

        public int AvailabilityID { get; set; }

        public decimal LeaseFloorArea { get; set; }

        public int ListingTypeID { get; set; }

        public decimal BasePrice { get; set; }

        public decimal CUSA { get; set; }

        public int HandOverConditionID { get; set; }

        public string ACCharges { get; set; }

        public string ACExtensionCharges { get; set; }

        public decimal EscalationRate { get; set; }

        public int MinimumLeaseTerm { get; set; }

        public decimal ParkingRent { get; set; }

        public DateTime? HandOverDate { get; set; }
    }
}