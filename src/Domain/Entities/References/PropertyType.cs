using apollo.Domain.Enums;
using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class PropertyType : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public PropertyCategory Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
