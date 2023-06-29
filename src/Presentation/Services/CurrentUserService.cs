using apollo.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Presentation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            _httpContextAccessor = httpContextAccessor;
        }

        public string Email { get; }
        public string UserId { get; }
    }
}
