using Books.Domain.Common.Models;

namespace Books.Domain.BookAggregate.ValueObjects
{
    public record BookId : IValueObject
    {
        public Guid Value { get; private set; }
        private BookId()
        {

        }
        private BookId(Guid value)
        {
            Value = value;
        }
        public static BookId Create(Guid value) => new(value);
        public static BookId CreateNew() => new(Guid.NewGuid());

    }
}
