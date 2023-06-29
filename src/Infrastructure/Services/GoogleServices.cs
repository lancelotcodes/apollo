using Geocoding;
using Geocoding.Google;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;

namespace apollo.Infrastructure.Services
{
    public class GoogleServices : IGoogleServices
    {
        const string API_KEY = "<GoogleAPIKey>";

        public async Task<GoogleGeoCodeResults> GetLocationCodingHttpClient(string address)
        {
            GoogleGeoCodeResults results = new GoogleGeoCodeResults();

            var client = new HttpClient();
            var path = "https://maps.googleapis.com/maps/api/geocode/json";

            path = $"{path}?address={address}&key={API_KEY}";

            try
            {
                var response = new HttpResponseMessage();

                response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<GoogleGeoCodeResults>(jsonString);
                        results = data;
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
            }catch (Exception err)
            {
                throw err;
            }

            return results;
        }

        public async Task<IEnumerable<Address>> GetLocationGeocoding(string address)
        {
            IGeocoder geocoder = new GoogleGeocoder() { ApiKey = API_KEY };

            try { 
                IEnumerable<Address> addresses = await geocoder.GeocodeAsync(address);
                return addresses;
            }
            catch(Exception err)
            {
                throw new GenericException(err.Message);
            }
        }
    }
}
