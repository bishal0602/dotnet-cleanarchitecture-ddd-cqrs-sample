using Books.Domain.BookAggregate.ValueObjects;
using Books.Domain.Common.Interfaces;

namespace Books.Domain.BookAggregate.Entities
{
    public class Author : IEntity<AuthorId>
    {
        private readonly IList<Book> _books = new List<Book>();
        public AuthorId Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? Bio { get; private set; }
        public IReadOnlyList<Book> Books => _books.ToList();

        private Author() { } // for ef core

        private Author(AuthorId id, string firstName, string lastName, string? bio)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Bio = bio;
        }

        public static Author Create(AuthorId id, string firstName, string lastName, string? bio) => new(id, firstName, lastName, bio);
        public static Author CreateNew(string firstName, string lastName, string? bio = null) => new(AuthorId.CreateNew(), firstName, lastName, bio);
        public void AddBook(Book book)
        {
            _books.Add(book);
        }

    }
}
