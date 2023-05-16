using Books.Application.Common;
using Books.Application.Contracts.Persistence;
using Books.Application.External.Models;
using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.ValueObjects;
using Books.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Books.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public BookRepository(BooksDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.Include(b => b.Authors).Include(b => b.Reviews).ToListAsync();
        }
        public async Task<PagedList<Book>> GetBooksAsync(int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Book> books = _context.Books
                                      .Include(b => b.Authors);
            return await books.CreatePagedListAsync(pageNumber, pageSize);

        }
        public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<BookId> bookIds)
        {
            return await _context.Books.Where(b => bookIds.Contains(b.Id))
                                       .Include(b => b.Reviews)
                                       .Include(b => b.Authors)
                                       .ToListAsync();
        }
        public async Task<Book?> GetBookByIdAsync(BookId bookId)
        {
            return await _context.Books.Include(b => b.Reviews)
                                       .Include(b => b.Authors)
                                       .FirstOrDefaultAsync(b => b.Id == bookId);
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public IAsyncEnumerable<Book> GetBooksAsAsyncEnumerble(CancellationToken cancellationToken)
        {
            return _context.Books.Include(b => b.Reviews)
                                      .Include(b => b.Authors)
                                      .AsAsyncEnumerable();
        }

        public async Task<BookCoverDto?> GetBookCoverAsync(BookId id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:52644/api/bookcovers/{id.Value}");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BookCoverDto>(content, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            return null;
        }

        public async Task<IEnumerable<BookCoverDto>> GetBookCoversProcessOneByOneAsync(BookId id, CancellationToken cancellationToken)
        {
            List<BookCoverDto> bookCovers = new();
            var bookCoverUrls = Enumerable.Range(1, 5).Select(x => $"http://localhost:52644/api/bookcovers/{id.Value}-dummycover{x}").ToList();

            HttpClient httpClient = _httpClientFactory.CreateClient();
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                using (var linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cancellationTokenSource.Token))
                {
                    foreach (var bookCoverUrl in bookCoverUrls)
                    {
                        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(bookCoverUrl, linkedCancellationTokenSource.Token);
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            string content = await httpResponseMessage.Content.ReadAsStringAsync(linkedCancellationTokenSource.Token);
                            BookCoverDto? bookCover = JsonSerializer.Deserialize<BookCoverDto>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                            if (bookCover is not null)
                            {
                                bookCovers.Add(bookCover);
                            }
                        }
                    }
                }
            }
            return bookCovers;
        }

        public async Task<IEnumerable<BookCoverDto>> GetBookCoversProcessAfterWaitForAllAsync(BookId id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            List<BookCoverDto> bookCovers = new();
            var bookCoverUrls = Enumerable.Range(1, 5).Select(x => $"http://localhost:52644/api/bookcovers/{id.Value}-dummycover{x}").ToList();

            List<Task<HttpResponseMessage>> list = new();
            List<Task<HttpResponseMessage>> bookCoverTasks = list;
            foreach (var bookCoverUrl in bookCoverUrls)
            {
                bookCoverTasks.Add(httpClient.GetAsync(bookCoverUrl));
            }

            HttpResponseMessage[] bookCoverTaskResponses = await Task.WhenAll(bookCoverTasks);

            foreach (var bookCoverTaskResponse in bookCoverTaskResponses)
            {
                string content = await bookCoverTaskResponse.Content.ReadAsStringAsync();
                BookCoverDto? bookCover = JsonSerializer.Deserialize<BookCoverDto>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (bookCover is not null)
                    bookCovers.Add(bookCover);
            }
            return bookCovers;
        }
    }
}
