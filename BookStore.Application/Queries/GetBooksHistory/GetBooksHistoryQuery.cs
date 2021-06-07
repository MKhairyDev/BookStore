using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;
using System;

namespace BookStore.Application.Queries.GetBooksHistory
{
   public class GetBooksHistoryQuery: QueryStringParameters, IRequest<PagedList<LoggedEvent>>
    {
        public string Description { get; set; }
        public string Action { get; set; }
    }
}
