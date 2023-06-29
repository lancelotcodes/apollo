using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.References
{
    public class Country : BaseEntityId
    {
        public Country()
        {
            States = new HashSet<State>();
            Regions = new HashSet<Region>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public virtual ICollection<State> States { get; private set; }
        public virtual ICollection<Region> Regions { get; private set; }

        public bool IsDeleted { get; set; }
    }
}
