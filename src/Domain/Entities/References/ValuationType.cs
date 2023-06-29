using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class ValuationType : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
