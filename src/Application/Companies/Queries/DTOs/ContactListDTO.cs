using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;

namespace apollo.Application.Companies.Queries.DTOs
{
    public class CompanyListDTO : IMapFrom<Company>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Company, CompanyListDTO>();
        }
    }
}
