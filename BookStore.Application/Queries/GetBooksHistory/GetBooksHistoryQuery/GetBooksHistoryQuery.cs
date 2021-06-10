using System;
using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory.GetBooksHistoryQuery
{
   public class GetBooksHistoryQuery: QueryStringParameters, IRequest<PagedList<LoggedEvent>>
    {
        public string Description { get; set; }
        public string Action { get; set; }
        public string BookId { get; set; }
    }
}
