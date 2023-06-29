using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateHandOverCondition
{
    public class CreateHandOverConditionCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
