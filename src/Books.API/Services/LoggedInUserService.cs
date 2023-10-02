using Books.Application.Contracts.Services;
using Books.Domain.UserAggregate.ValueObjects;
using System.Security.Claims;

namespace Books.API.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserId? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                    return null;
                return UserId.Create(new Guid(userId));
            }
        }

    }
}
