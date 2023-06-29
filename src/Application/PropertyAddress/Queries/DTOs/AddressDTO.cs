using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.Shared;
using apollo.Domain.Enums;
using AutoMapper;

namespace apollo.Application.PropertyAddress.Queries.DTOs
{
    public class AddressDTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int? SubMarketID { get; set; }
        public string SubMarketName { get; set; }
        public int? MicroDistrictID { get; set; }
        public string MicroDistrictName { get; set; }
        public string ZipCode { get; set; }
        public AddressTag AddressTag { get; set; }
        public string AddressTagName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PolygonPoints { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, AddressDTO>()
                .ForMember(i => i.ID, o => o.MapFrom(m => m.Address.ID))
                .ForMember(i => i.PropertyID, o => o.MapFrom(m => m.ID))
                .ForMember(i => i.Longitude, o => o.MapFrom(m => m.Address.Longitude))
                .ForMember(i => i.Latitude, o => o.MapFrom(m => m.Address.Latitude))
                .ForMember(i => i.PolygonPoints, o => o.MapFrom(m => m.Address.PolygonPoints))
                .ForMember(i => i.Line1, o => o.MapFrom(m => m.Address.Line1))
                .ForMember(i => i.Line2, o => o.MapFrom(m => m.Address.Line2))
                .ForMember(i => i.CityID, o => o.MapFrom(m => m.Address.CityID))
                .ForMember(i => i.ZipCode, o => o.MapFrom(m => m.Address.ZipCode))
                .ForMember(i => i.SubMarketID, o => o.MapFrom(m => m.Address.SubMarketID))
                .ForMember(i => i.MicroDistrictID, o => o.MapFrom(m => m.Address.MicroDistrictID))
                .ForMember(i => i.AddressTag, o => o.MapFrom(m => m.Address.AddressTag))
                .ForMember(i => i.CityName, o => o.MapFrom(m => m.Address.City.Name))
                .ForMember(i => i.SubMarketName, o => o.MapFrom(m => m.Address.SubMarket.Name))
                .ForMember(i => i.MicroDistrictName, o => o.MapFrom(m => m.Address.MicroDistrict.Name))
                .ForMember(i => i.AddressTagName, o => o.MapFrom(x => x.Address.AddressTag.ToString()));
        }
    }

    public class AddressShortDTO : IMapFrom<Address>
    {
        public int ID { get; set; }
        public string Line1 { get; set; }
        public string CityName { get; set; }
        public string SubMarketName { get; set; }
        public string MicroDistrictName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Address, AddressShortDTO>()
                .ForMember(i => i.CityName, o => o.MapFrom(m => m.City.Name))
                .ForMember(i => i.SubMarketName, o => o.MapFrom(m => m.SubMarket.Name))
                .ForMember(i => i.MicroDistrictName, o => o.MapFrom(m => m.MicroDistrict.Name));
        }
    }
}
