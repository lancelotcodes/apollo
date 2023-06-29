using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;

namespace apollo.Application.Contacts.Queries.DTOs
{
    public class ContactListDTO : IMapFrom<Contact>
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactListDTO>();
        }
    }
}
