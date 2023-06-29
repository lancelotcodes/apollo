using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using apollo.Domain.Entities.References;
using AutoMapper;

namespace apollo.Application.Grades.DTOs
{
    public class ShortGradeListDTO : IMapFrom<Property>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Grade, ShortGradeListDTO>();
        }
    }
}
