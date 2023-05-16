using Books.Application.Contracts.Persistence;
using Books.Domain.BookAggregate;

namespace Books.Application.Books.Queries.GetBookListAsStream
{
    public class GetBookListAsStreamQueryHandler : IRequestHandler<GetBookListAsStreamQuery, Result<IAsyncEnumerable<Book>, Error>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookListAsStreamQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public async Task<Result<IAsyncEnumerable<Book>, Error>> Handle(GetBookListAsStreamQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask; // mediatr needs me to return Task
            try
            {
                IAsyncEnumerable<Book> books = _bookRepository.GetBooksAsAsyncEnumerble(cancellationToken);
                return Result.Success<IAsyncEnumerable<Book>, Error>(books); // REVIEW: there must be a better way to do this
            }
            catch (OperationCanceledException)
            {
                return new TaskCancelledError("Streaming of book was cancelled");
            }



        }
    }
}
