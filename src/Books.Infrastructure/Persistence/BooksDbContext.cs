using Books.Application.Contracts.Services;
using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;
using Books.Domain.Common;
using Books.Domain.Common.Interfaces;
using Books.Domain.UserAggregate;
using Books.Domain.UserAggregate.ValueObjects;
using Books.Infrastructure.Persistence.Configuration;
using Books.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Books.Infrastructure.Persistence
{
    public class BooksDbContext : DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;
        private readonly AuditableInterceptor _auditableInterceptor;

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookReview> BookReviews { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<AuthorBook> AuthorBooks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public BooksDbContext(DbContextOptions<BooksDbContext> options, IDateTimeProvider dateTimeProvider, IPasswordHasher<User> passwordHasher, PublishDomainEventInterceptor publishDomainEventInterceptor, AuditableInterceptor auditableInterceptor) : base(options)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _passwordHasher = passwordHasher;
            _publishDomainEventInterceptor = publishDomainEventInterceptor;
            _auditableInterceptor = auditableInterceptor;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventInterceptor, _auditableInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(BooksDbContext).Assembly);

            AuditableConfiguration.Configure(modelBuilder);

            DataSeed.Seed(modelBuilder, _passwordHasher, _dateTimeProvider);
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
