using Shared.Domain.Common;

namespace apollo.Domain.Entities.Core
{
    public class Lead : AuditableEntity, BaseEntityId
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
        public string SourceURL { get; set; }
        public int LeadSourceID { get; set; }
        public int LeadStatusID { get; set; }
        public int DealID { get; set; }
        public string Country { get; set; }
        public string IPAddress { get; set; }
        public int LeadCategoryID { get; set; }
        public string Background { get; set; }
        public string DesiredLocation { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
    }
}
