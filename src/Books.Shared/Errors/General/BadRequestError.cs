using Microsoft.AspNetCore.Http;

namespace Books.Shared.Errors.General
{
    public class BadRequestError : Error
    {
        protected BadRequestError(string errorType = nameof(BadRequestError), int statusCode = StatusCodes.Status400BadRequest, string? errorMessage = null) : base(errorType, statusCode, errorMessage)
        {
        }
    }
}
