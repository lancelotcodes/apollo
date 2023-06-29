using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class OwnershipType : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
