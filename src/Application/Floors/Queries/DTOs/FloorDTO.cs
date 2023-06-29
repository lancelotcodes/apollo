using apollo.Application.Common.Mappings;
using apollo.Application.Units.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using System.Collections.Generic;

namespace apollo.Application.Floors.Queries.DTOs
{
    public class FloorDTO : IMapFrom<Floor>
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public decimal FloorPlateSize { get; set; }
        public IEnumerable<UnitDTO> Units { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Floor, FloorDTO>()
                .ForMember(i => i.Units, o => o.MapFrom(m => m.Units))
                .ForMember(i => i.BuildingID, o => o.MapFrom(m => m.BuildingID))
                .ForMember(i => i.Name, o => o.MapFrom(m => m.Name))
                .ForMember(i => i.FloorPlateSize, o => o.MapFrom(m => m.FloorPlateSize))
                .ForMember(i => i.Units, o => o.MapFrom(m => m.Units));
        }
    }
}
