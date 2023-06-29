using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Domain.Entities.References
{
    public class HandOverCondition
    {
        public int ID { get; set; }
        
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
