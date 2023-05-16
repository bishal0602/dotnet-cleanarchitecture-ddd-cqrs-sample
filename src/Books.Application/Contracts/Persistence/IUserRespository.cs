using Books.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Contracts.Persistence
{
    public interface IUserRespository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUserNameAsync(string userName);
        void AddUser(User user);
        Task<bool> SaveChangesAsync();

    }
}
