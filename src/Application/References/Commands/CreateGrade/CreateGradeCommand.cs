using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateGrade
{
    public class CreateGradeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int PropertyTypeID { get; set; }
    }
}
