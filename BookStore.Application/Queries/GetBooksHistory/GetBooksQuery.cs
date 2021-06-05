using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;
using System;

namespace BookStore.Application.Queries.GetBooksHistory
{
   public class GetBooksQuery: QueryStringParameters, IRequest<PagedList<LoggedEvent>>
    {
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string Action { get; set; }
    }
}
