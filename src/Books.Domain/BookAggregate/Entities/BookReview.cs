using Books.Domain.BookAggregate.ValueObjects;
using Books.Domain.Common;

namespace Books.Domain.BookAggregate.Entities
{
    public class BookReview : Entity<BookReviewId>
    {
        public BookReviewId Id { get; private set; }
        public string Username { get; private set; }
        public string Comment { get; private set; }
        public BookId BookId { get; private set; }
        private BookReview() { } // for ef core
        private BookReview(BookReviewId id, string username, string comment, BookId bookId)
        {
            Id = id;
            Username = username;
            Comment = comment;
            BookId = bookId;
        }
        public static BookReview Create(BookReviewId id, string username, string comment, BookId bookId) => new(id, username, comment, bookId);
        public static BookReview CreateNew(string username, string comment, BookId bookId) => new(BookReviewId.CreateNew(), username, comment, bookId);

    }
}
