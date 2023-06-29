using apollo.Domain.Enums;
using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class PropertyDocument : AuditableEntity, BaseEntityId
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public Property Property { get; set; }
        public int DocumentID { get; set; }
        public Document Document { get; set; }
        public bool IsPrimary { get; set; }
        public PropertyDocumentType? DocumentType { get; set; }
    }
}
