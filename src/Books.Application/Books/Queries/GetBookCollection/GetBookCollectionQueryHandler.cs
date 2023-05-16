using Books.Application.Contracts.Persistence;
using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.ValueObjects;

namespace Books.Application.Books.Queries.GetBookCollection
{
    public class GetBookCollectionQueryHandler : IRequestHandler<GetBookCollectionQuery, Result<IEnumerable<Book>, Error>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookCollectionQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public async Task<Result<IEnumerable<Book>, Error>> Handle(GetBookCollectionQuery request, CancellationToken cancellationToken)
        {
            List<BookId> bookIds = request.bookIds.Select(bId => BookId.Create(bId)).ToList();

            IEnumerable<Book> books = await _bookRepository.GetBooksAsync(bookIds);

            if (books.Count() != bookIds.Count)
            {
                List<Guid> bookIdsAdded = books.Select(a => a.Id.Value).ToList();
                List<Guid> bookIdsNotAdded = bookIds.Where(b => !bookIdsAdded.Contains(b.Value))
                                                    .Select(b => b.Value)
                                                    .ToList();
                return new NotFoundError($"Books with following Ids were not found: {string.Join(" , ", bookIdsNotAdded)}");
            }

            return books.ToList();
        }
    }
}
