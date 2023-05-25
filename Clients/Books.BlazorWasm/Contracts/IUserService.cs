using Books.BlazorWasm.Models;

namespace Books.BlazorWasm.Contracts
{
    public interface IUserService
    {
        Task<User?> GetUserFromLocalStorageAsync();
        Task RemoveUserFromLocalStorageAsync();
        Task<User?> SendAuthenticateRequestAsync(string email, string password);
    }
}
