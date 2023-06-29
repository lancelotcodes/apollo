using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Enums;
using AutoMapper;
using System.Linq;

namespace apollo.Application.Properties.Queries.DTOs
{
    public class MapPropertyListDTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
        public PropertyCategory Category { get; set; }
        public string CategoryName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public int? SubMarketID { get; set; }
        public string SubMarketName { get; set; }
        public string MainImage { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Property, MapPropertyListDTO>()
                .ForMember(i => i.PropertyTypeName, o => o.MapFrom(m => m.PropertyType.Name))
                .ForMember(i => i.Longitude, o => o.MapFrom(m => m.Address.Longitude))
                .ForMember(i => i.Latitude, o => o.MapFrom(m => m.Address.Latitude))
                .ForMember(i => i.Category, o => o.MapFrom(m => m.PropertyType.Category))
                .ForMember(i => i.CategoryName, o => o.MapFrom(x => x.PropertyType.Category.ToString()))
                .ForMember(i => i.MainImage, o => o.MapFrom(m => m.PropertyDocuments.FirstOrDefault(x => x.IsPrimary == true
                                        && x.IsDeleted == false
                                        && x.DocumentType == PropertyDocumentType.MainImage).Document.DocumentPath))
                .ForMember(i => i.CityID, o => o.MapFrom(m => m.Address.CityID))
                .ForMember(i => i.CityName, o => o.MapFrom(m => m.Address.City.Name))
                .ForMember(i => i.SubMarketID, o => o.MapFrom(m => m.Address.SubMarketID))
                .ForMember(i => i.SubMarketName, o => o.MapFrom(m => m.Address.SubMarket.Name));
        }
    }
}
