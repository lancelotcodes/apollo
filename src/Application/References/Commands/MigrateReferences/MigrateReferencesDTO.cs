using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.References.Commands.MigrateReferences
{
    public class MigrateReferencesDTO
    {
        public string Name { get; set; }
        public string Entity { get; set; }
        public string ParentEntity { get; set; }
        public string PolygonPoints { get; set; }
    }
}
