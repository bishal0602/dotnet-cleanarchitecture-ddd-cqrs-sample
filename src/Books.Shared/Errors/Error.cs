using Microsoft.AspNetCore.Http;

namespace Books.Shared.Errors
{
    public class Error
    {
        public string ErrorType { get; set; }
        public int? StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public Error(string errorType = "InternalServerError", int? statusCode = StatusCodes.Status500InternalServerError, string? errorMessage = null)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }
    }
}
