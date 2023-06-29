namespace apollo.Application.Common.Models
{
    public class SaveEntityResult
    {
        public SaveEntityResult()
        {
        }

        public SaveEntityResult(int entityId)
        {
            EntityId = entityId;
        }

        public int EntityId { get; set; }
    }
}
