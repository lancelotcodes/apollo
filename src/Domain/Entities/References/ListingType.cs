using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class ListingType : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
