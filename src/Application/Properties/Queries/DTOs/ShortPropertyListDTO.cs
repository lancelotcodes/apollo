using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;

namespace apollo.Application.Properties.Queries.DTOs
{
    public class ShortPropertyListDTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, ShortPropertyListDTO>();
        }
    }
}
