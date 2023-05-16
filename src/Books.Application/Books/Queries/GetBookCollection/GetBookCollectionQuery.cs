using Books.Domain.BookAggregate;

namespace Books.Application.Books.Queries.GetBookCollection
{
    public record GetBookCollectionQuery(IEnumerable<Guid> bookIds) : IRequest<Result<IEnumerable<Book>, Error>>;
}
