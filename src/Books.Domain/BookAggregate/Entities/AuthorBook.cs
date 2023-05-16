using Books.Domain.BookAggregate.ValueObjects;

namespace Books.Domain.BookAggregate.Entities
{
    public class AuthorBook
    {
        public AuthorId AuthorId { get; private set; }
        public BookId BookId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        private AuthorBook() { } // for ef core
        private AuthorBook(AuthorId authorId, BookId bookId, DateTime createdAt)
        {
            AuthorId = authorId;
            BookId = bookId;
            CreatedAt = createdAt;
        }
        public static AuthorBook Create(AuthorId authorId, BookId bookId, DateTime createdAt) => new(authorId, bookId, createdAt);
        public static AuthorBook CreateNew(AuthorId authorId, BookId bookId) => new(authorId, bookId, DateTime.UtcNow);
    }
}
