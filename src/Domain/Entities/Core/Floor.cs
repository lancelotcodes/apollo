using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.Core
{
    public class Floor : AuditableEntity, BaseEntityId
    {
        public Floor()
        {
            Units = new HashSet<Unit>();
        }
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public virtual Building Building { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public decimal FloorPlateSize { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
    }
}
