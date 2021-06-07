using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory
{
   public class GetPagedBooksHistoryQueryHandler : IRequestHandler<PagedQuery, PagedDataTableResponse<IEnumerable<LoggedEvent>>>
    {
        private readonly IBookEventRepository _eventRepository;

        public GetPagedBooksHistoryQueryHandler(IBookEventRepository eventRepository)
        {
            _eventRepository = eventRepository??throw new ArgumentNullException(nameof(eventRepository));
        }
        public async Task<PagedDataTableResponse<IEnumerable<LoggedEvent>>> Handle(PagedQuery request, CancellationToken cancellationToken)
        {
            var validFilter = new GetBooksHistoryQuery();

            //Add Pagination
            AddPaginationInfoToQuery(request, ref validFilter);

            // Map order -> OrderBy
            AddOrderByToQuery(request, ref validFilter);

            // Map Search > searchable columns
            AddSearchFiltrationToQuery( request, ref validFilter);
        
            // query based on filter
            var res= await _eventRepository.GetBookHistoryAsync(validFilter);

            // response wrapper
            return new PagedDataTableResponse<IEnumerable<LoggedEvent>>(res.data.Data, request.Draw, res.recordsCount);
        }

        private void AddPaginationInfoToQuery(PagedQuery request, ref GetBooksHistoryQuery validFilter)
        {
            {
                // Draw map to PageNumber
                // Length map to PageSize
                validFilter.PageNumber = (request.Start / request.Length) + 1;
                validFilter.PageSize = request.Length;
            };
        }

        private void AddSearchFiltrationToQuery(PagedQuery request,ref GetBooksHistoryQuery validFilter)
        {
            if (!string.IsNullOrEmpty(request.Search.Value))
            {
                validFilter.Action = request.Search.Value;
                validFilter.Description = request.Search.Value;
            }
        }

        private  void AddOrderByToQuery(PagedQuery request,ref GetBooksHistoryQuery validFilter)
        {
            var colOrder = request.Order[0];
            switch (colOrder.Column)
            {
                case 0:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "Id" : "Id DESC";
                    break;

                case 1:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "Action" : "Action DESC";
                    break;

                case 2:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "Description" : "Description DESC";
                    break;

                case 3:
                    validFilter.OrderBy = colOrder.Dir == "asc" ? "TimeStamp" : "TimeStamp DESC";
                    break;
            }
        }
    }
}
