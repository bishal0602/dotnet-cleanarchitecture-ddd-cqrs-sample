using Books.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Authentication.Common
{
    public record AuthenticationResponse(User User, string Token)
    {

    }
}
