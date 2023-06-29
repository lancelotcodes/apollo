using apollo.Domain.Entities.References;
using Shared.Domain.Common;
using System;

namespace apollo.Domain.Entities.Core
{
    public class Contract : AuditableEntity, BaseEntityId
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyID { get; set; }
        public virtual Property Property { get; set; }
        public int? CompanyID { get; set; }
        public virtual Company Company { get; set; }
        public int? ContactID { get; set; } // future implementation
        public virtual Contact Contact { get; set; } // future implementation
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TenantClassificationID { get; set; }
        public virtual TenantClassification TenantClassification { get; set; }
        public decimal ClosingRate { get; set; }
        public decimal EstimatedArea { get; set; }
        public int LeaseTerm { get; set; }
        public int? BrokerID { get; set; } // future implementation
        public virtual Contact Broker { get; set; } // future implementation
        public int? BrokerCompanyID { get; set; } // future implementation
        public virtual Company BrokerCompany { get; set; } // future implementation
        public bool IsHistorical { get; set; }
    }
}
