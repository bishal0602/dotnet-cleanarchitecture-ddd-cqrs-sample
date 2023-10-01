using Books.Application.Contracts.Persistence;
using Books.Application.External.Models;
using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.ValueObjects;

namespace Books.Application.Books.Queries.GetBookDetail
{
    public class GetBookDetailQueryHandler
        : IRequestHandler<GetBookDetailQuery, Result<(Book?, IEnumerable<BookCoverDto>?), Error>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookDetailQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<(Book?, IEnumerable<BookCoverDto>?), Error>> Handle(GetBookDetailQuery request, CancellationToken cancellationToken)
        {
            BookId bookId = BookId.Create(request.Id);

            Book? book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book is null)
            {
                return new NotFoundError($"Book with Id {bookId} was not found");
            }

            if (!request.IncludeCover)
                return (book, null);

            try
            {
                var bookCovers = await _bookRepository.GetBookCoversProcessOneByOneAsync(bookId, cancellationToken);
                return (book, bookCovers);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Task was cancelled for fetching book of Id {bookId}");
                return new TaskCancelledError($"Task was cancelled for fetching book of Id {bookId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // TODO: use logger
                return (book, new List<BookCoverDto>());
            }



        }
    }
}
