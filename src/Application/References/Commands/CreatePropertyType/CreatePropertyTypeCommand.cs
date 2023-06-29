using apollo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
