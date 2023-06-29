using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;
using AutoMapper;

namespace apollo.Application.PropertyTypes.Queries.DTOs
{
    public class PropertyTypeDTO : IMapFrom<PropertyType>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PropertyType, PropertyTypeDTO>();
        }
    }
}
