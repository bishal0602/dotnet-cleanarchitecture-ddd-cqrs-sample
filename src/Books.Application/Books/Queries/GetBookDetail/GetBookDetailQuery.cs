using Books.Application.External.Models;
using Books.Domain.BookAggregate;

namespace Books.Application.Books.Queries.GetBookDetail
{
    public record GetBookDetailQuery(Guid Id, bool IncludeCover)
        : IRequest<Result<(Book?, IEnumerable<BookCoverDto>?), Error>>;
}
