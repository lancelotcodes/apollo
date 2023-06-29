using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateMicrodistrict
{
    public class CreateMicrodistrictCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int CityID { get; set; }
    }
}
