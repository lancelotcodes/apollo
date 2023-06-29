using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class Availability : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
