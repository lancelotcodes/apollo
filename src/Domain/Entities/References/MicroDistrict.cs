using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class MicroDistrict : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
        public bool IsDeleted { get; set; }
    }
}
