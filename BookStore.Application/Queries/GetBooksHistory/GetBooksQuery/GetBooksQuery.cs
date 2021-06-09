using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory.GetBooksQuery
{
    public class GetBooksQuery : QueryStringParameters, IRequest<PagedList<Book>>
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }
}