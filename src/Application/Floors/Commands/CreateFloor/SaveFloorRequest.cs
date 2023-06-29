using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;

namespace apollo.Application.Floors.Commands.CreateFloor
{
    public class SaveFloorRequest : IRequest<SaveEntityResult>, IMapFrom<Floor>
    {
        public int ID { get; set; }
        public int BuildingID { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public decimal FloorPlateSize { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SaveFloorRequest, Floor>(MemberList.Source);
        }
    }
}
