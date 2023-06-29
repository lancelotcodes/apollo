using apollo.Domain.Entities.References;
using apollo.Domain.Enums;
using Shared.Domain.Common;

namespace apollo.Domain.Entities.Shared
{
    public class Address : AuditableEntity, BaseEntityId
    {

        public Address()
        {

        }

        public int ID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public int CityID { get; set; }
        public virtual City City { get; set; }
        public int? SubMarketID { get; set; }
        public virtual SubMarket SubMarket { get; set; }
        public int? MicroDistrictID { get; set; }
        public virtual MicroDistrict MicroDistrict { get; set; }
        public string ZipCode { get; set; }
        public AddressTag AddressTag { get; set; }

        //for GEO Coding
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string PolygonPoints { get; set; }
    }
}
