using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace apollo.Application.States.Queries.GetStates
{
    public class GetStateQuery : IRequest<IEnumerable<StateDTO>>
    {
        [JsonIgnore]
        public int CountryID { get; set; }
    }
}
