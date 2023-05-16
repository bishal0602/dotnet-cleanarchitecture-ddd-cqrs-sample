using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Shared.Errors.Authentication
{
    public abstract class AuthenticationError : Error
    {
        public AuthenticationError(string errorType, int statusCode = StatusCodes.Status400BadRequest, string? errorMessage = null) : base(errorType, statusCode, errorMessage)
        {

        }
    }
}
