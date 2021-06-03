using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Commands.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Domain.Entities;

namespace BookStore.Application.Queries.Interfaces.Repositories
{
    public interface IBookRepository:IRepository<Book>
    {
        Task<IEnumerable<LoggedEvent>> GetBooksHistoryAsync(QueryStringParameters queryParameters);    }
}
