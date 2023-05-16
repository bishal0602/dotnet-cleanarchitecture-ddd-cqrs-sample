using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Shared.Errors.Authentication
{
    public class InvalidCredentialsError : AuthenticationError
    {
        public InvalidCredentialsError(string errorMessage) : base("Authentication.InvalidCredentials", errorMessage: errorMessage) { }
    }
}
