using apollo.Application.Common.Mappings;
using apollo.Application.Units.Queries.DTOs;
using apollo.Domain.Entities.Core;
using AutoMapper;
using System.Collections.Generic;

namespace apollo.Application.Floors.Queries.DTOs
{
    public class FloorShortDetailDTO : IMapFrom<Floor>
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public decimal FloorPlateSize { get; set; }
        public IEnumerable<UnitShortDetailDTO> Units { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Floor, FloorShortDetailDTO>();
        }
    }
}
