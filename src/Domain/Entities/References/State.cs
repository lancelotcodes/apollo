using Shared.Domain.Common;

namespace apollo.Domain.Entities.References
{
    public class State : BaseEntityId
    {
        public State()
        {
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int CountryID { get; set; }
        public Country Country { get; set; }
        public bool IsDeleted { get; set; }
    }
}
