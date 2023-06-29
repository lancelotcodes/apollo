using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.Core
{
    public class Contact : AuditableEntity
    {
        public Contact()
        {
            PropertyListed = new HashSet<Property>();
            PropertyOwned = new HashSet<Property>();
            Contracts = new HashSet<Contract>();
            BrokerContracts = new HashSet<Contract>();
        }
        public int ID { get; set; }
        public int? CompanyID { get; set; }
        public Company Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int HubSpotID { get; set; }
        public string LinkedIn { get; set; }
        public string ContactProfile { get; set; }

        public virtual ICollection<Property> PropertyListed { get; private set; }

        public virtual ICollection<Property> PropertyOwned { get; private set; }

        public virtual ICollection<Contract> Contracts { get; private set; }

        public virtual ICollection<Contract> BrokerContracts { get; private set; }
    }
}
