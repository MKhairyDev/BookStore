using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory
{
   public class GetBooksHistoryQueryHandler: IRequestHandler<GetBooksQuery, PagedList<LoggedEvent>>
    {
        public Task<PagedList<LoggedEvent>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
