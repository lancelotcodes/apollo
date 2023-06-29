using Shared.Domain.Common;
using System;

namespace apollo.Domain.Entities.Core
{
    public class Mandate : BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyID { get; set; }
        public int AttachmentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Property Property { get; set; }
        public virtual Document Attachment { get; set; }
    }
}
