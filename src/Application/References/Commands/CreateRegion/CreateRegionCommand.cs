using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.CreateRegion
{
    public class CreateRegionCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string PolygonPoints { get; set; }
        public int CountryID { get; set; }
    }
}
