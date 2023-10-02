

using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;
using Books.Domain.Common;
using Books.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;

namespace Books.Infrastructure.Persistence.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => BookId.Create(value));

            builder.HasMany(e => e.Reviews)
                .WithOne()
                .HasForeignKey(e => e.BookId)
                .IsRequired();

            builder.Navigation(e => e.Reviews).HasField("_bookReviews").UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(e => e.Authors)
                .WithMany(e => e.Books)
                .UsingEntity<AuthorBook>(configureJoinEntityType =>
                {
                    configureJoinEntityType.HasKey(e => new { e.AuthorId, e.BookId });

                });


            builder.Navigation(e => e.Authors).HasField("_authors").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
