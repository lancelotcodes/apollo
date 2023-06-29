using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;
using AutoMapper;

namespace apollo.Application.Lookups.DTOs
{
    public class SubMarketDTO : IMapFrom<SubMarket>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int CityID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SubMarket, SubMarketDTO>();
        }
    }
}
