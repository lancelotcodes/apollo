using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.References
{
    public class Region : BaseEntityId
    {
        public Region()
        {
            Provinces = new HashSet<Province>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Province> Provinces { get; private set; }
        public bool IsDeleted { get; set; }
    }
}
