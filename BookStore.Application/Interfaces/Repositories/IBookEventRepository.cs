using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Parameters;
using BookStore.Application.Queries.GetBooksHistory;
using BookStore.Application.Queries.GetBooksHistory.GetBooksHistoryQuery;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Repositories
{
    public interface IBookEventRepository
    {
        Task<(PagedList<LoggedEvent> data, RecordsCount recordsCount)> GetBookHistoryAsync(GetBooksHistoryQuery queryParameters);

    }
}