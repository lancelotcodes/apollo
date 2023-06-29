using apollo.Domain.Entities.References;
using apollo.Domain.Entities.Shared;
using Shared.Domain.Common;
using System.Collections.Generic;

namespace apollo.Domain.Entities.Core
{
    public class Property : AuditableEntity, BaseEntityId
    {
        public Property()
        {
            Agents = new HashSet<PropertyAgent>();
            Contracts = new HashSet<Contract>();
            PropertyDocuments = new HashSet<PropertyDocument>();
            Mandates = new HashSet<Mandate>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int? MasterProjectID { get; set; }
        public int? MasterPropertyID { get; set; }
        public int? ResidentialUnitID { get; set; }
        public int? ListingID { get; set; }
        public virtual Property MasterProject { get; set; }
        public virtual Property MasterProperty { get; set; }
        public virtual Property ResidentialUnit { get; set; }
        public virtual Property Listing { get; set; }

        // Property.Id= Building.PropertyId 
        public virtual Building Building { get; set; }

        // Proppery.MasterPropertyID= Property.Id
        public virtual ICollection<Property> Buildings { get; set; }

        //Property.Building.Id= Unit
        public int PropertyTypeID { get; set; }
        public PropertyType PropertyType { get; set; }
        public int? SubTypeID { get; set; }
        public SubType SubType { get; set; }
        public int? GradeID { get; set; }
        public Grade Grade { get; set; }
        public int? AddressID { get; set; }
        public Address Address { get; set; }
        public int? ContactID { get; set; }
        public virtual Contact Contact { get; set; }
        public int? OwnerID { get; set; }
        public virtual Contact Owner { get; set; }
        public int? OwnerCompanyID { get; set; }
        public virtual Company OwnerCompany { get; set; }
        public virtual ICollection<Appraisal> Appraisals { get; set; }
        public virtual ICollection<PropertyAgent> Agents { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<PropertyDocument> PropertyDocuments { get; set; }
        public virtual ICollection<Mandate> Mandates { get; set; }
        public bool IsExclusive { get; set; }
        public string Note { get; set; }
        public int? SEOID { get; set; }
        public SEO SEO { get; set; }

    }
}
