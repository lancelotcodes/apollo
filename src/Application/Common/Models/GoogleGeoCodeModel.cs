using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Common.Models
{
    public class GoogleGeoCodeResults
    {
        public string error_message { get; set; }
        public IEnumerable<GoogleGeoCodeModel> results { get; set; }
        public string status { get; set; }
    }
    public class GoogleGeoCodeModel
    {
        public IEnumerable<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }

    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public IEnumerable<string> types { get; set; }
    }

    public class Geometry
    {
        public GeoLocation location { get; set; }
    }

    public class GeoLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }


}
