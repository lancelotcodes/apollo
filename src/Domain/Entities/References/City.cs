using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.References
{
    public class City : BaseEntityId
    {
        public City()
        {
            SubMarkets = new HashSet<SubMarket>();
            MicroDistricts = new HashSet<MicroDistrict>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int ProvinceID { get; set; }
        public Province Province { get; set; }
        public ICollection<SubMarket> SubMarkets { get; private set; }
        public ICollection<MicroDistrict> MicroDistricts { get; private set; }
        public bool IsDeleted { get; set; }
    }
}
