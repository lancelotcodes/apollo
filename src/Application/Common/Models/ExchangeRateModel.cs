using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Common.Models
{
    public class ExchangeRateResponseModel
    {
        public bool success { get; set; }
        public long timestamp { get; set; }

        [JsonProperty("base")]
        public string base_currency {get;set;}
        public string date { get; set; }
        public Dictionary<string, object> rates { get; set; }
    }
}
