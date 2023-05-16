using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Persistence.Configuration
{
    public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> builder)
        {
            builder.ToTable("BookReviews");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(id => id.Value, value => BookReviewId.Create(value));
        }
    }
}
