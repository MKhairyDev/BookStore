using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Application.Queries.GetBooksHistory;
using BookStore.Application.Queries.GetBooksHistory.GetBooksHistoryQuery;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.EventStore.SqlServer.Contexts;
using LinqKit;

namespace BookStore.Infrastructure.EventStore.SqlServer.Repositories
{
    public class BookEventRepository : GenericEventRepository<LoggedEvent>, IBookEventRepository
    {
        public BookEventRepository(LoggedEventDbContext context) : base(context)
        {
        }

        public async Task<(PagedList<LoggedEvent> data, RecordsCount recordsCount)> GetBookHistoryAsync(
            GetBooksHistoryQuery queryParameters)
        {
            if (queryParameters == null) throw new ArgumentNullException(nameof(queryParameters));

            // Setup IQueryable
            var collection = Context.BooksEvents.AsExpandable();
            var recordsTotal = collection.Count();

            // Apply Filtering
            ColumnFiltration(ref collection, queryParameters);
            var recordsFiltered = collection.Count();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };
            // Apply order by using "Linq.Dynamic.Core" library for dynamic Linq
            if (!string.IsNullOrWhiteSpace(queryParameters.OrderBy))
                collection = collection.OrderBy(queryParameters.OrderBy);

            var data = await PagedList<LoggedEvent>.CreateAsync(collection,
                queryParameters.PageNumber,
                queryParameters.PageSize);
            return (data, recordsCount);
        }

        private void ColumnFiltration(ref IQueryable<LoggedEvent> loggedEvents, GetBooksHistoryQuery queryParameters)
        {
            if (!loggedEvents.Any() || queryParameters == null)
                return;

            if (string.IsNullOrEmpty(queryParameters.Description) &&
                string.IsNullOrEmpty(queryParameters.Action))
                return;

            var predicate = PredicateBuilder.New<LoggedEvent>();

            if (!string.IsNullOrEmpty(queryParameters.Action))
                predicate = predicate.Or(p => p.Action.Contains(queryParameters.Action.Trim()));

            if (!string.IsNullOrEmpty(queryParameters.Description))
                predicate = predicate.Or(p => p.Description.Contains(queryParameters.Description.Trim()));

            loggedEvents = loggedEvents.Where(predicate);
        }
    }
}