using Books.Application.Contracts.Services;
using Books.Domain.BookAggregate;
using Books.Domain.BookAggregate.Entities;
using Books.Domain.BookAggregate.ValueObjects;
using Books.Domain.Common;
using Books.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Books.Infrastructure.Persistence
{
    public class BooksDbContext : DbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookReview> BookReviews { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<AuthorBook> AuthorBooks { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public BooksDbContext(DbContextOptions<BooksDbContext> options, IDateTimeProvider dateTimeProvider, ILoggedInUserService loggedInUserService, IPasswordHasher<User> passwordHasher) : base(options)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _loggedInUserService = loggedInUserService;
            _passwordHasher = passwordHasher;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case (EntityState.Added):
                        entry.Entity.CreatedOn = _dateTimeProvider.Now;
                        entry.Entity.CreatedBy = _loggedInUserService.UserName;
                        break;
                    case (EntityState.Modified):
                        entry.Entity.LastModifiedOn = _dateTimeProvider.Now;
                        entry.Entity.LastModifiedBy = _loggedInUserService.UserName;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksDbContext).Assembly);
            DataSeed.Seed(modelBuilder, _passwordHasher, _dateTimeProvider);
            base.OnModelCreating(modelBuilder);
        }
    }
}
