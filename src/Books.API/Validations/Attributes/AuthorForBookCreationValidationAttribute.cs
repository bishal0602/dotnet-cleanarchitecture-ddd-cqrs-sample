using Books.API.Models.BookDtos;
using System.ComponentModel.DataAnnotations;

namespace Books.API.Validations.Attributes
{
    public class AuthorForBookCreationValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var author = value as AuthorForBookCreationDto;
            if (author == null)
            {
                return new ValidationResult("Value is not of type AuthorBookCommand");
            }

            if (author.Id == null && (author.FirstName == null || author.LastName == null))
            {
                return new ValidationResult("Either Id or both FirstName and LastName must be provided for Author");
            }

            return ValidationResult.Success;
        }
    }
}
