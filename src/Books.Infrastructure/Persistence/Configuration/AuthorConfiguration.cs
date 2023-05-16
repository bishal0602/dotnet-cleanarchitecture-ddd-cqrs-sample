using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infrastructure.Persistence.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(id => id.Value, value => AuthorId.Create(value));

            builder.Navigation(e => e.Books).HasField("_books").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
