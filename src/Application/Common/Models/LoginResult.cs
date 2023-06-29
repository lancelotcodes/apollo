using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Common.Models
{
    public class LoginResult
    {
        public string Token { get; set; }
        public UserProfileDTO User { get; set; }
        public string[] Roles { get; set; }
    }

    public class UserDTO
    {
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
