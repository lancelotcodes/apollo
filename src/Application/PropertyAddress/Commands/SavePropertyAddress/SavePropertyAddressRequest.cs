using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Shared;
using apollo.Domain.Enums;
using AutoMapper;
using MediatR;

namespace apollo.Application.PropertyAddress.Commands.SavePropertyAddress
{
    public class SavePropertyAddressRequest : IRequest<SaveEntityResult>, IMapFrom<Address>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int CityID { get; set; }
        public int? SubMarketID { get; set; }
        public int? MicroDistrictID { get; set; }
        public string ZipCode { get; set; }
        public AddressTag AddressTag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PolygonPoints { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SavePropertyAddressRequest, Address>();
        }
    }
}
