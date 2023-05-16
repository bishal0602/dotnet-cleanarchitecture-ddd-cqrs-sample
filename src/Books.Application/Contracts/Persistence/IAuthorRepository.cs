using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;

namespace Books.Application.Contracts.Persistence
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorById(AuthorId authorId);
        Task<List<Author>> GetAuthorsById(List<AuthorId> authorIds);
        void AddAuthor(Author author);
        Task<bool> ExistsAuthorAsync(AuthorId authorId);
        Task<bool> SaveChangesAsync();

    }
}
