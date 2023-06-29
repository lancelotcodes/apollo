using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class PropertyImageVersion : BaseEntityId
    {
        public int ID { get; set; }
        public string ImageLink { get; set; }
        public int Width { get; set; }

        public int PropertyImageID { get; set; }
    }
}
