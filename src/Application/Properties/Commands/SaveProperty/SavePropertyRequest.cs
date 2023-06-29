using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;

namespace apollo.Application.Properties.Commands.SaveProperty
{
    public class SavePropertyRequest : IRequest<SaveEntityResult>, IMapFrom<Property>
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public int PropertyTypeID { get; set; }
        public int? MasterPropertyID { get; set; }
        public int? ContactID { get; set; }
        public int? OwnerID { get; set; }
        public int? GradeID { get; set; }
        public int? OwnerCompanyID { get; set; }
        public bool IsExclusive { get; set; }
        public string Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SavePropertyRequest, Property>(MemberList.Source);
        }
    }
}
