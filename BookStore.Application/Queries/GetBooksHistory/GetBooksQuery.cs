using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory
{
   public class GetBooksQuery: QueryStringParameters, IRequest<PagedList<LoggedEvent>>
    {

    }
}
