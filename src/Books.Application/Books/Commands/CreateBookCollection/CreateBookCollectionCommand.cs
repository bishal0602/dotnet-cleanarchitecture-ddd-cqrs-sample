using Books.Application.Books.Commands.CreateBook;
using Books.Domain.BookAggregate;

namespace Books.Application.Books.Commands.CreateBookCollection
{
    public record CreateBookCollectionCommand(List<CreateBookCommand> BookCollection) : IRequest<Result<IEnumerable<Book>, Error>>;
}
