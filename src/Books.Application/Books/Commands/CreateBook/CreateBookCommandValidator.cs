using FluentValidation;

namespace Books.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(b => b.Description).MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters");


            RuleFor(a => a.Authors).NotNull().WithMessage("Authors cannot be null").SetValidator(new AuthorsForCreateBookValidator());
        }
    }
    public class AuthorsForCreateBookValidator : AbstractValidator<List<AuthorBookCommand>>
    {
        public AuthorsForCreateBookValidator()
        {
            RuleFor(a => a).ForEach(a =>
            {
                a.NotNull().WithMessage("Author cannot be null").SetValidator(new AuthorForCreateBookValidator());
            });
        }
    }
    public class AuthorForCreateBookValidator : AbstractValidator<AuthorBookCommand>
    {
        public AuthorForCreateBookValidator()
        {
            RuleFor(a => a).Must(a =>
            {
                if (a.Id is null && (string.IsNullOrWhiteSpace(a.FirstName) || string.IsNullOrWhiteSpace(a.LastName)))
                    return false;
                return true;
            }).WithMessage("Either Id for existing author or FirstName LastName for non existing author must be provided");
        }
    }
}
