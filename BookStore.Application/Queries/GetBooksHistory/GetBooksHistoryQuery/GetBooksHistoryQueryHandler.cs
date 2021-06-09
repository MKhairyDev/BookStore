using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory.GetBooksHistoryQuery
{
   public class GetBooksHistoryQueryHandler: IRequestHandler<GetBooksHistoryQuery, PagedList<LoggedEvent>>
    {
        private readonly IBookEventRepository _eventRepository;

        public GetBooksHistoryQueryHandler(IBookEventRepository eventRepository)
        {
            _eventRepository = eventRepository??throw new ArgumentNullException(nameof(eventRepository));
        }
        public async Task<PagedList<LoggedEvent>> Handle(GetBooksHistory.GetBooksHistoryQuery.GetBooksHistoryQuery request, CancellationToken cancellationToken)
        {
          var res= await _eventRepository.GetBookHistoryAsync(request);
          return res.data;
        }
    }
}
