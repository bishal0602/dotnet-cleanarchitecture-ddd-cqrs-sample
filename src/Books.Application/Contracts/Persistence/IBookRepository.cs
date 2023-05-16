using Books.Domain.BookAggregate.ValueObjects;
using Books.Domain.BookAggregate;
using Books.Application.External.Models;
using Books.Application.Common;

namespace Books.Application.Contracts.Persistence
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        Task<Book?> GetBookByIdAsync(BookId bookId);
        IAsyncEnumerable<Book> GetBooksAsAsyncEnumerble(CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<PagedList<Book>> GetBooksAsync(int pageNumber = 1, int pageSize = 10);

        Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<BookId> bookIds);
        Task<BookCoverDto?> GetBookCoverAsync(BookId id);
        Task<IEnumerable<BookCoverDto>> GetBookCoversProcessOneByOneAsync(BookId id, CancellationToken cancellationToken);
        Task<IEnumerable<BookCoverDto>> GetBookCoversProcessAfterWaitForAllAsync(BookId id);

        Task<bool> SaveChangesAsync();
    }
}
