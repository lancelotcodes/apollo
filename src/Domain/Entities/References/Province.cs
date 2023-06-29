using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.References
{
    public class Province : BaseEntityId
    {
        public Province()
        {
            Cities = new HashSet<City>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int RegionID { get; set; }
        public Region Region { get; set; }
        public ICollection<City> Cities { get; private set; }
        public bool IsDeleted { get; set; }
    }
}
