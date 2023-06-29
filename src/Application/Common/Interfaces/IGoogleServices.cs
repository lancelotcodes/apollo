using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Models;
using Geocoding;

namespace apollo.Application.Common.Interfaces
{
    public interface IGoogleServices
    {
        Task<IEnumerable<Address>> GetLocationGeocoding(string address);
        Task<GoogleGeoCodeResults> GetLocationCodingHttpClient(string address);
    }
}
