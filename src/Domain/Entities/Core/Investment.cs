using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class Investment : AuditableEntity, BaseEntityId
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int PropertyID { get; set; }

        public virtual Property Property { get; set; }

        public double LotArea { get; set; }

        public double FloorArea { get; set; }

        public double LeasableArea { get; set; }

        public double TypicalFloorPlate { get; set; }

        public double CommonArea { get; set; }

        public double Frontage { get; set; }

        public double CeilingHeight { get; set; }

        public string FloorAreaRatio { get; set; }

        public string TotalFloors { get; set; }

        public string TotalRooms { get; set; }

        public string TotalElevators { get; set; }

        public string TotalParkingSlots { get; set; }

        public string EfficiencyRate { get; set; }

        public string HandoverCondition { get; set; }

        public string TaxDeclarationClassification { get; set; }

        public float YearCompleted { get; set; }

        public string BuildingClass { get; set; }

        public string PEZAStatus { get; set; }

        public string BackupPower { get; set; }

        public string AirConditioningSystem { get; set; }

        public string TelecommunicationProviders { get; set; }

        public string DensityRatio { get; set; }

        public string Amenities { get; set; }

        public string Availability { get; set; }

        public double GrossSellingPrice { get; set; }

        public double NetSellingPrice { get; set; }

        public double BaseGrossSellingPrice { get; set; }

        public double BaseNetSellingPrice { get; set; }

        public double BaseRent { get; set; }

        public double AssociationDues { get; set; }

        public double ParkingSlotLeaseRate { get; set; }

        public double AirConditioningCharges { get; set; }

        public string LeaseTermYears { get; set; }

        public string AnnualEscalation { get; set; }

        public double Commission { get; set; }
    }
}
