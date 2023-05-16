using Microsoft.AspNetCore.Http;

namespace Books.Shared.Errors.General
{
    public class ValidationError : Error
    {
        public ValidationError() : base(nameof(ValidationError), StatusCodes.Status400BadRequest)
        {
            ValidationErrorDictionary = new();
        }
        public ValidationError(string key, string errorMessage) : this()
        {
            ValidationErrorDictionary.Add(key, errorMessage);
        }
        public Dictionary<string, string> ValidationErrorDictionary { get; set; }
    }
}
