using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;

namespace apollo.Application.Countries.Commands.CreateCountry
{
    public class AddCountryCommand : IRequest<int>, IMapFrom<Country>
    {
        public string Name { get; set; }
        public IEnumerable<AddCountryStateDTO> States { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddCountryCommand, Country>();
            profile.CreateMap<AddCountryStateDTO, State>();
            profile.CreateMap<AddCountryCityDTO, City>();
        }
    }
    public class AddCountryStateDTO : IMapFrom<State>
    {
        public string Name { get; set; }
        public IEnumerable<AddCountryCityDTO> Cities { get; set; }
    }
    public class AddCountryCityDTO : IMapFrom<City>
    {
        public string Name { get; set; }
    }
}
