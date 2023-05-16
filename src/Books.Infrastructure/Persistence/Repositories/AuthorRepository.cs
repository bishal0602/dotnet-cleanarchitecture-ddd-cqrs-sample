using Books.Application.Contracts.Persistence;
using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Persistence.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BooksDbContext _context;

        public AuthorRepository(BooksDbContext context)
        {
            _context = context;
        }
        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
        }

        public async Task<bool> ExistsAuthorAsync(AuthorId authorId)
        {
            return await _context.Authors.AnyAsync(a => a.Id == authorId);
        }

        public async Task<Author?> GetAuthorById(AuthorId authorId)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<List<Author>> GetAuthorsById(List<AuthorId> authorIds)
        {
            return await _context.Authors.Where(a => authorIds.Contains(a.Id)).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
