using Books.Domain.BookAggregate;

namespace Books.Application.Books.Commands.CreateBook
{
    public record CreateBookCommand(string Title, string? Description, List<AuthorBookCommand> Authors) : IRequest<Result<Book, Error>>;
    public record AuthorBookCommand(Guid? Id, string? FirstName, string? LastName, string? Bio);
}
