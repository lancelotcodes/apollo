using Microsoft.AspNetCore.Identity;

namespace Shared.Domain.Common
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
