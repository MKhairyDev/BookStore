using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Application.Queries.GetBooksHistory.GetBooksQuery;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Persistence.SqlServer.Contexts;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence.SqlServer.Repositories
{
    public class BookRepository: GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context):base(context)
        {
          
        }
        public async Task<Book> GetBookWithAuthorAsync(int id)
        {
            return await Context.Books.Include(x => x.Authors).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(PagedList<Book> data, RecordsCount recordsCount)> GetBooksAsync(GetBooksQuery queryParameters)
        {
            if (queryParameters == null) throw new ArgumentNullException(nameof(queryParameters));

            // Setup IQueryable
            var collection = Context.Books.Include(x=>x.Authors).AsExpandable();
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

            var data = await PagedList<Book>.CreateAsync(collection,
                queryParameters.PageNumber,
                queryParameters.PageSize);
            return (data, recordsCount);
        }

        private void ColumnFiltration(ref IQueryable<Book> books, GetBooksQuery queryParameters)
        {
            if (!books.Any() || queryParameters == null)
                return;

            if (string.IsNullOrEmpty(queryParameters.AuthorName) &&
                string.IsNullOrEmpty(queryParameters.Title))
                return;

            var predicate = PredicateBuilder.New<Book>();

            if (!string.IsNullOrEmpty(queryParameters.AuthorName))
                predicate = predicate.Or(p => p.Authors.Exists(x=>x.Name.Contains(queryParameters.AuthorName.Trim())));

            if (!string.IsNullOrEmpty(queryParameters.Title))
                predicate = predicate.Or(p => p.Title.Contains(queryParameters.Title.Trim()));

            books = books.Where(predicate);
        }

    }
}
