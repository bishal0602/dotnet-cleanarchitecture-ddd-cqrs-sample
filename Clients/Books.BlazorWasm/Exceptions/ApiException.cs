using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.BlazorWasm.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(ProblemDetails? problemDetails)
        {
            ProblemDetails = problemDetails ?? new ProblemDetails() { Type = "ApiError", Detail = "Api call resulted in error", Status = StatusCodes.Status500InternalServerError };
        }
        public ProblemDetails ProblemDetails { get; }
    }
}
