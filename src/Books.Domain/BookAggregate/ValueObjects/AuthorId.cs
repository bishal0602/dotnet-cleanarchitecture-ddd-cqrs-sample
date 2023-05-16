using Books.Domain.Common.Models;

namespace Books.Domain.BookAggregate.ValueObjects
{
    public record AuthorId : IValueObject
    {
        public Guid Value { get; private set; }
        private AuthorId()
        {

        }
        private AuthorId(Guid value)
        {
            Value = value;
        }
        public static AuthorId Create(Guid value) => new(value);
        public static AuthorId CreateNew() => new(Guid.NewGuid());
    }
}
