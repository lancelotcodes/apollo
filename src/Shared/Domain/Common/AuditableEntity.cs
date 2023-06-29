using System;

namespace Shared.Domain.Common
{
    public abstract class AuditableEntity : ISoftDeletableEntity
    {
        public int? LegacyID { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }

    public interface ISoftDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
    public interface BaseEntityId
    {
        public int ID { get; set; }
    }

}
