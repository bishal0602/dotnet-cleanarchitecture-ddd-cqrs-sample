using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Shared.Errors.Authentication
{
    public class UserNameAlreadyTakenError : AuthenticationError
    {
        public UserNameAlreadyTakenError(string errorMessage = "Username is already taken.") : base("Authentication.UserNameAlreadyTaken", errorMessage: errorMessage) { }
    }
}
