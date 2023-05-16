using Books.Domain.Common.Models;

namespace Books.Domain.BookAggregate.ValueObjects
{
    public record BookReviewId : IValueObject
    {
        public Guid Value { get; private set; }
        private BookReviewId() { }
        private BookReviewId(Guid value)
        {
            Value = value;
        }

        public static BookReviewId CreateNew() => new(Guid.NewGuid());
        public static BookReviewId Create(Guid value) => new(value);
    }
}
