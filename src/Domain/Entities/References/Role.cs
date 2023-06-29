using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class Role : BaseEntityId
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public bool ViewOnly { get; set; }
        public bool Broker { get; set; }
        public bool Publisher { get; set; }
        public bool Editor { get; set; }
        public bool Administrator { get; set; }
        public bool SystemAdmin { get; set; }
        public bool Management { get; set; }
    }
}
