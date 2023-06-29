using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.Core
{
    public class Company : AuditableEntity, BaseEntityId
    {
        public Company()
        {
            Contacts = new HashSet<Contact>();
            DevelopedBuildings = new HashSet<Building>();
            PropertyManagedBuildings = new HashSet<Building>();
            LeasedBuildings = new HashSet<Building>();
            Contracts = new HashSet<Contract>();
            BrokerContracts = new HashSet<Contract>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyLabel { get; set; }
        public int? CompanyStatusID { get; set; }
        public int? SectorID { get; set; }
        public int? IndustryGroupID { get; set; }
        public int? IndustryID { get; set; }
        public int? BusinessID { get; set; }
        public int? OriginID { get; set; }
        public int? HubSpotID { get; set; }
        public string CompanyLogo { get; set; }
        public decimal? CreditRatings { get; set; }
        public string CreditRating { get; set; }
        public int? SubsidiaryID { get; set; }
        public string LinkedIn { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Building> DevelopedBuildings { get; set; }
        public virtual ICollection<Building> PropertyManagedBuildings { get; set; }
        public virtual ICollection<Building> LeasedBuildings { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Contract> BrokerContracts { get; private set; }

    }
}
