using apollo.Domain.Enums;
using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class PropertyAgent : BaseEntityId
    {
        public int ID { get; set; }
        public int AgentID { get; set; }
        public Contact Agent { get; set; }
        public int PropertyID { get; set; }
        public Property Property { get; set; }
        public AgentType AgentType { get; set; }
        public bool IsVisibleOnWeb { get; set; }
        public bool IsDeleted { get; set; }
    }
}
