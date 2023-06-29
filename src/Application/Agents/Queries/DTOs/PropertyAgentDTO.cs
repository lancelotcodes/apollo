using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;

namespace apollo.Application.Agents.Queries.DTOs
{
    public class PropertyAgentDTO : IMapFrom<PropertyAgent>
    {
        public int ID { get; set; }
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string AgentEmail { get; set; }
        public string AgentPhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public int PropertyID { get; set; }
        public AgentType AgentType { get; set; }
        public bool IsVisibleOnWeb { get; set; }
        public bool IsDeleted { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PropertyAgent, PropertyAgentDTO>()
                .ForMember(x => x.AgentName, d => d.MapFrom(y => y.Agent.FirstName + " " + y.Agent.LastName))
                .ForMember(x => x.AgentPhoneNumber, d => d.MapFrom(y => y.Agent.PhoneNumber))
                .ForMember(x => x.AgentEmail, d => d.MapFrom(y => y.Agent.Email));
        }
    }

    public class ContactDTO : IMapFrom<Contact>
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactDTO>();
        }
    }
}
