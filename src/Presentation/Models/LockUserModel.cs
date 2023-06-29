using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Models
{
    public class LockUserModel
    {
        public string Email { get; set; }
        public DateTimeOffset? LockUntil { get; set; }
    }
}
