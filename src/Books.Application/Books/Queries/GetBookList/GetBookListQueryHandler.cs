using Books.Application.Common;
using Books.Application.Contracts.Persistence;
using Books.Domain.BookAggregate;

namespace Books.Application.Books.Queries.GetBookList
{
    public class GetBookListQueryHandler
        : IRequestHandler<GetBookListQuery, Result<PagedList<Book>, Error>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookListQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public async Task<Result<PagedList<Book>, Error>> Handle(GetBookListQuery request, CancellationToken cancellationToken)
        {
            PagedList<Book> books = await _bookRepository.GetBooksAsync(request.PageNumber, request.PageSize);
            return books;
        }
    }
}
