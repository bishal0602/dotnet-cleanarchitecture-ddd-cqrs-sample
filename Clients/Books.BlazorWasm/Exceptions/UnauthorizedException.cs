using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.BlazorWasm.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(ProblemDetails problemDetails) : base(problemDetails) { }
        public UnauthorizedException() : base(new ProblemDetails
        {
            Detail = "Unauthorized rescource",
            Status = StatusCodes.Status401Unauthorized,
            Title = "Authentication.Unauthorized"
        })
        { }
    }
}
