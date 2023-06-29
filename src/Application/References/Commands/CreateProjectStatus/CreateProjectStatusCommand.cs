using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateProjectStatus
{
    public class CreateProjectStatusCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
