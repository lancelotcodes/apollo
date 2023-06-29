using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Models
{
    public class ChangePassword
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set;  }
        public string ConfirmPassword { get; set; }
    }
}
