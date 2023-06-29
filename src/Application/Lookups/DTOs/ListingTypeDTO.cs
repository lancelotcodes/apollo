using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;
using AutoMapper;

namespace apollo.Application.Lookups.DTOs
{
    public class ListingTypeDTO : IMapFrom<ListingType>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListingType, ListingTypeDTO>();
        }
    }
}
