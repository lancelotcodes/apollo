using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace apollo.Infrastructure.HangFire
{
    public class AuthFilter : IDashboardAuthorizationFilter
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
