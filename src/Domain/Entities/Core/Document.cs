using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class Document : ISoftDeletableEntity, BaseEntityId
    {
        public int ID { get; set; }
        public string DocumentKey { get; set; }
        public DocSourceType SourceType { get; set; }
        public string DocumentName { get; set; }
        public long DocumentSize { get; set; }
        public string DocumentPath { get; set; }
        public int? ThumbNailId { get; set; }
        public Document ThumbNail { get; set; }
        public bool IsDeleted { get; set; }
    }
}
