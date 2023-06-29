using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;

namespace apollo.Application.Buildings.Queries.DTOs
{
    public class SimpleBuildingDTO : IMapFrom<Building>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MainImage { get; set; }
        public double Distance { get; set; }
        public int SortOrder { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Building, SimpleBuildingDTO>()
                .ForMember(i => i.Address, o => o.MapFrom(m => m.Property.Address.Line1))
                .ForMember(i => i.Longitude, o => o.MapFrom(m => m.Property.Address.Longitude));
        }
    }
}
