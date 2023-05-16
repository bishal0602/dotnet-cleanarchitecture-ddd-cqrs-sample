using Books.Domain.BookAggregate;

namespace Books.Application.Books.Queries.GetBookListAsStream
{
    public class GetBookListAsStreamQuery : IRequest<Result<IAsyncEnumerable<Book>, Error>>
    {
    }
}
