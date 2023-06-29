using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Domain.Entities.References
{
    public class Business
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int IndustryID { get; set; }
        public int IndustryGroupID { get; set; }
        public int SectorID { get; set; }

    }
}
