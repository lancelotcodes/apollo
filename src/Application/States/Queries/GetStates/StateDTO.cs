using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.References;

namespace apollo.Application.States.Queries.GetStates
{
    public class StateDTO : IMapFrom<State>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<State, StateDTO>();
        }
    }
}
