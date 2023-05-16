using Microsoft.AspNetCore.Http;

namespace Books.Shared.Errors.General
{
    public class NotFoundError : Error
    {

        public NotFoundError(
            string errorType = nameof(NotFoundError),
            string? errorMessage = null) : base(errorType, StatusCodes.Status404NotFound, errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
