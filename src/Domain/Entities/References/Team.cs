using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class Team : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HeadID { get; set; }
        public int DepartmentID { get; set; }
    }
}
