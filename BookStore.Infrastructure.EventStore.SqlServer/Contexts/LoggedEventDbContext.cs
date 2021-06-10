using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Infrastructure.EventStore.SqlServer.Contexts
{
    public class LoggedEventDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public LoggedEventDbContext(DbContextOptions<LoggedEventDbContext> options,
            ILoggerFactory loggerFactory
        ) : base(options)
        {
            /*
             No tracking queries are useful when the results are used in a read-only scenario. 
            They're quicker to execute because there's no need to set up the change tracking information.
            If you don't need to update the entities retrieved from the database, 
            then a no-tracking query should be used. 
            */
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _loggerFactory = loggerFactory;

        }

        public DbSet<LoggedEvent> BooksEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}