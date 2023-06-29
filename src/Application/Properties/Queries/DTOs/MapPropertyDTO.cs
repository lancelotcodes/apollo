using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;

namespace apollo.Application.Properties.Queries.DTOs
{
    public class MapPropertyDTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public PropertyCategory Category { get; set; }
        public string CategoryName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? GradeID { get; set; }
        public string GradeName { get; set; }
        public int? AddressID { get; set; }
        public string Line1 { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int? SubMarketID { get; set; }
        public string SubMarketName { get; set; }
        public int? SEOID { get; set; }
        public string SEOUrl { get; set; }
        public string MainImage { get; set; }
        public int BuildingID { get; set; }
        public PEZAStatus? PEZA { get; set; }
        public string PEZAName { get; set; }
        public int YearBuilt { get; set; }
        public int? DeveloperID { get; set; }
        public string DeveloperName { get; set; }
        public int? LeasingContactID { get; set; }
        public string LeasingContactName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, MapPropertyDTO>()
                .ForMember(i => i.PropertyTypeName, o => o.MapFrom(m => m.PropertyType.Name))
                .ForMember(i => i.Longitude, o => o.MapFrom(m => m.Address.Longitude))
                .ForMember(i => i.Latitude, o => o.MapFrom(m => m.Address.Latitude))
                .ForMember(i => i.Category, o => o.MapFrom(m => m.PropertyType.Category))
                .ForMember(i => i.GradeName, o => o.MapFrom(m => m.Grade.Name))
                .ForMember(i => i.Line1, o => o.MapFrom(m => m.Address.Line1))
                .ForMember(i => i.CityID, o => o.MapFrom(m => m.Address.CityID))
                .ForMember(i => i.CityName, o => o.MapFrom(m => m.Address.City.Name))
                .ForMember(i => i.SubMarketID, o => o.MapFrom(m => m.Address.SubMarketID))
                .ForMember(i => i.SubMarketName, o => o.MapFrom(m => m.Address.SubMarket.Name))
                .ForMember(i => i.SEOUrl, o => o.MapFrom(m => m.SEO.Url))
                .ForMember(i => i.CategoryName, o => o.MapFrom(x => x.PropertyType.Category.ToString()))
                .ForMember(i => i.BuildingID, o => o.MapFrom(m => m.Building.ID))
                .ForMember(i => i.PEZA, o => o.MapFrom(m => m.Building.PEZA))
                .ForMember(i => i.PEZAName, o => o.MapFrom(x => x.Building.PEZA.ToString()))
                .ForMember(i => i.YearBuilt, o => o.MapFrom(m => m.Building.YearBuilt))
                .ForMember(i => i.DeveloperID, o => o.MapFrom(m => m.Building.DeveloperID))
                .ForMember(i => i.DeveloperName, o => o.MapFrom(m => m.Building.Developer.Name))
                .ForMember(i => i.LeasingContactID, o => o.MapFrom(m => m.Building.LeasingContactID))
                .ForMember(i => i.LeasingContactName, o => o.MapFrom(m => m.Building.LeasingContact.Name));
        }
    }
}
