using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;
using Books.Domain.Common;
using Books.Domain.Common.Interfaces;

namespace Books.Domain.BookAggregate
{
    public class Book : AuditableEntity, IAggregateRoot<BookId>
    {
        private readonly IList<BookReview> _bookReviews = new List<BookReview>();
        private readonly IList<Author> _authors = new List<Author>();
        public BookId Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public IReadOnlyList<BookReview> Reviews => _bookReviews.ToList();
        public IReadOnlyList<Author> Authors => _authors.ToList();

        private Book() { } // for ef core

        private Book(BookId id, string title, string? description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
        public static Book Create(BookId id, string title, string? description) => new(id, title, description);
        public static Book CreateNew(string title, string? description) => new(BookId.CreateNew(), title, description);

        public void AddAuthor(Author author)
        {
            _authors.Add(author);
            author.AddBook(this);
        }
    }
}
