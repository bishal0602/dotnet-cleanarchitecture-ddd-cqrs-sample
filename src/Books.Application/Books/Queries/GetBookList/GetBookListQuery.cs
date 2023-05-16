using Books.Application.Common;
using Books.Domain.BookAggregate;

namespace Books.Application.Books.Queries.GetBookList
{
    public record GetBookListQuery(int PageNumber, int PageSize) : IRequest<Result<PagedList<Book>, Error>>;
}
