using Books.Application.Authentication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Authentication.Commands.Register
{
    public record RegisterCommand(string FirstName, string LastName, string UserName, string Email, string Password) : IRequest<Result<AuthenticationResponse, Error>>
    {
    }
}
