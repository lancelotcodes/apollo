using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Models;

namespace apollo.Application.Common.Interfaces
{
    public interface ICurrencyService
    {
        Task<ExchangeRateResponseModel> GetCurrentExchangeRate(string baseCurrency);
    }
}
