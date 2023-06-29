using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;

namespace apollo.Application.Countries.Queries.GetCountryById
{
    public class CountryDTO : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
      
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryDTO>();
        }
    }
}
