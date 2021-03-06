using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Infrastructure.Persistence.SqlServer.Contexts
{
    public class BookStoreDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ILoggerFactory _loggerFactory;

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options,
            IDateTimeService dateTime,
            ILoggerFactory loggerFactory
            ) : base(options)
        {

            _dateTime = dateTime;
            _loggerFactory = loggerFactory;
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(SeedAuthorData());
        }
        //Todo: create seed dervice & move this there.
        private List<Author> SeedAuthorData()
        {
            return new List<Author>()
            {
                new Author
                {
                    Id = 1,
                    Created = default,
                    LastModified = null,
                    Name = "Uncle Bob",
                },
                new Author
                {
                    Id = 2,
                    Created = default,
                    LastModified = null, 
                    Name = "Martin Fowler"
                },
                new Author
                {
                    Id = 3,
                    Created = default,
                    Name = "Martin Kleppmann"
                }
            };
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}
