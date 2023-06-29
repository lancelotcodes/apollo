using System.Collections.Generic;

namespace apollo.Application.Common.Models
{

    public class UserProfileBaseDTO
    {
        public string Id { get; set; }
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UserProfileDTO : UserProfileBaseDTO
    {
        public bool IsVerified { get; set; }
        public bool IsLocked { get; set; }
        public string Role { get; set; }
        public IEnumerable<ClaimsDTO> Claims { get; set; }
    }

    public class ClaimsDTO
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
