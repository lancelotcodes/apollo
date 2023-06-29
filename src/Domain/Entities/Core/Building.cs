using apollo.Domain.Entities.References;
using apollo.Domain.Enums;
using Shared.Domain.Common;
using System;
using System.Collections.Generic;

namespace apollo.Domain.Entities.Core
{
    public class Building : AuditableEntity, BaseEntityId
    {
        public Building()
        {
            Floors = new HashSet<Floor>();
        }

        public int ID { get; set; }

        #region Public Properties

        public string Name { get; set; }

        public int PropertyID { get; set; }

        public virtual Property Property { get; set; }

        public virtual ICollection<Floor> Floors { get; set; }

        public DateTime? DateBuilt { get; set; }

        public int YearBuilt { get; set; }

        public PEZAStatus PEZA { get; set; }

        public bool OperatingHours { get; set; }

        public string LEED { get; set; }


        #endregion

        #region Private Properties

        public int? LeasingContactID { get; set; }

        public virtual Company LeasingContact { get; set; }

        public int? DeveloperID { get; set; }

        public virtual Company Developer { get; set; }

        public int? PropertyManagementID { get; set; }

        public virtual Company PropertyManagement { get; set; }

        public int? OwnershipTypeID { get; set; }

        public virtual OwnershipType OwnershipType { get; set; }

        public int? ProjectStatusID { get; set; }

        public virtual ProjectStatus ProjectStatus { get; set; }

        public DateTime? TurnOverDate { get; set; }

        public string TenantMix { get; set; }

        public double GrossBuildingSize { get; set; }

        public string GrossLeasableSize { get; set; }

        public string TypicalFloorPlateSize { get; set; }

        public int TotalFloors { get; set; }

        public int TotalUnits { get; set; }

        public decimal EfficiencyRatio { get; set; }

        public string CeilingHeight { get; set; }

        public string MinimumLeaseTerm { get; set; }

        public string Elevators { get; set; }

        public string Power { get; set; }

        public string ACSystem { get; set; }

        public string Telcos { get; set; }

        public string Amenities { get; set; }

        public string WebPage { get; set; }

        public double DensityRatio { get; set; }

        public string ParkingElevator { get; set; }

        public string PassengerElevator { get; set; }

        public string ServiceElevator { get; set; }

        #endregion
    }

}
