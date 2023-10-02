using Books.Domain.UserAggregate.ValueObjects;

namespace Books.Application.Contracts.Services
{
    public interface ILoggedInUserService
    {
        UserId? UserId { get; }
    }
}
