using apollo.Application.Common.Mappings;
using apollo.Domain.Entities.Core;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;

namespace apollo.Application.PropertyMandate.Commands.SavePropertyMandate
{
    public class SavePropertyMandateRequest : IRequest<bool>
    {
        public List<PropertyMandateRequest> Mandates { get; set; }
    }

    public class PropertyMandateRequest : IMapFrom<Mandate>
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public int AttachmentId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PropertyMandateRequest, Mandate>();
        }
    }
}
