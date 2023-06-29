using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Common.Models
{
    public class GenerateResetPasswordResult
    {
        public string UserId { get; set; }
        public string Token { get; set; }

    }

    public class ResetPasswordModel
    {
        public string FirstName { get; set; }
        public string ResetLink { get; set; }
    }
}
