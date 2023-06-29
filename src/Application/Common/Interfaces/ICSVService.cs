using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Common.Interfaces
{
    public interface ICSVService
    {
        Task<List<Dictionary<string, object>>> ReadCSV(IFormFile file);
    }
}
