using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateOwnershipType
{
    public class CreateOwnershipTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
