using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;

namespace apollo.Application.States.Commands.CreateState
{
    public class CreateStateCommand : IRequest<int>, IMapFrom<State>
    {
        [JsonIgnore]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public IEnumerable<AddStateCityDTO> Cities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStateCommand, State>();
            profile.CreateMap<AddStateCityDTO, City>();
        }
    }
    public class AddStateCityDTO : IMapFrom<City>
    {
        public string Name { get; set; }
    }
}
