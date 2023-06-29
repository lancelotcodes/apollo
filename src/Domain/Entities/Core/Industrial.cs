using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class Industrial : BaseEntityId
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int PropertyID { get; set; }

        public virtual Property Property { get; set; }

        public int ProviderClass { get; set; }

        public string ZoningClassification { get; set; }

        public string OccupancyClassification { get; set; }

        public double TotalGrossLeasableSizeSQM { get; set; }

        public double TGLSOpenArea { get; set; }

        public double TGLSCoveredArea { get; set; }

        public bool Accommodate10W { get; set; }

        public int Total10WSlots { get; set; }

        public int StructureType { get; set; }

        public bool WithCargoLifts { get; set; }

        public bool WithOfficeComponents { get; set; }

        public string TotalBaysDocks { get; set; }

        public int TypeBayDock { get; set; }

        public string PowerSupplyCapacity { get; set; }

        public string FloorLoadingCapacity { get; set; }

        public string WaterSupply { get; set; }

        public string WaterSewageTreatment { get; set; }

        public string VentilationSystem { get; set; }

        public string InternetServiceProvider { get; set; }

        public string IndustyTypeAllowed { get; set; }

        public string AnyRestriction { get; set; }

    }
}
