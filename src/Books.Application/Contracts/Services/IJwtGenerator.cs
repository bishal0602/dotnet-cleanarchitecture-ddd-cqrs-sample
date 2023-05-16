using Books.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Contracts.Services
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
