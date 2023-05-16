using Books.Domain.UserAggregate;

namespace Books.API.Models.Authentication
{
    public class AuthResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
