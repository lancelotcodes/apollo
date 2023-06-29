using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class Grade : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int PropertyTypeID { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}
