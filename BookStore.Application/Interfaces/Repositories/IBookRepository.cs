using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.Parameters;
using BookStore.Application.Queries.GetBooksHistory;
using BookStore.Application.Queries.GetBooksHistory.GetBooksQuery;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Repositories
{
   public interface IBookRepository:IGenericCrudRepository<Book>
    {
        Task<Book> GetBookWithAuthorAsync(int id);
        Task<(PagedList<Book> data, RecordsCount recordsCount)> GetBooksAsync(GetBooksQuery queryParameters);

    }
}
