using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;

namespace apollo.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        const string access_key = "2d4bdfa0f9c52a01675efdb2064c95e9";
        public async Task<ExchangeRateResponseModel> GetCurrentExchangeRate(string baseCurrency)
        {
            ExchangeRateResponseModel results = new ExchangeRateResponseModel();

            var client = new HttpClient();

            var baseURI = "http://data.fixer.io/api/latest";

            var path = $"{baseURI}?access_key={access_key}";

            try
            {
                var response = new HttpResponseMessage();

                response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ExchangeRateResponseModel>(jsonString);
                        results = data;
                    }
                    catch (Exception err)
                    {
                        throw err;
                    }
                }
            } catch (Exception err)
            {
                throw err;
            }

            return results;
        }
    }
}
