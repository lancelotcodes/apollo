using Microsoft.AspNetCore.Identity;
using Shared.Domain.Enums;

namespace Shared.Domain.Common
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsVerified { get; set; }
        public string JobTitle { get; set; }
        public string BusinessPhone { get; set; }
        public string HomePhone { get; set; }
        public string Address { get; set; }
        public string URL { get; set; }
        public string Notes { get; set; }
        public string ProfilePicture { get; set; }
        public int ManagerID { get; set; }
        public int CompanyId { get; set; }
        public int TeamID { get; set; }
        public int DepartmentID { get; set; }
        public AccessLevel AccessLevelID { get; set; }
    }
}
