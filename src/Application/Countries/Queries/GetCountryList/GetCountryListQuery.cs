using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Countries.Queries.GetCountryById;

namespace apollo.Application.Countries.Queries.GetCountryList
{
    public class GetCountryListQuery : IRequest<IEnumerable<CountryDTO>>
    {
    }
}
