using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Shared.Errors.Authentication
{
    public class EmailAlreadyInUseError : AuthenticationError
    {
        public EmailAlreadyInUseError(string errorMessage = "Email is already in use.") : base("Authentication.EmailIsAlreadyInUse", errorMessage: errorMessage) { }
    }
}
