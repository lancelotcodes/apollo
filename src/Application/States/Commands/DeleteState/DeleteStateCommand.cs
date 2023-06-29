using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.States.Commands.DeleteState
{
    public class DeleteStateCommand : IRequest
    {
        public int ID { get; set; }
    }
}
