using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;
using AutoMapper;

namespace apollo.Application.Lookups.DTOs
{
    public class TenantClassificationDTO : IMapFrom<TenantClassification>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TenantClassification, TenantClassificationDTO>();
        }
    }
}
