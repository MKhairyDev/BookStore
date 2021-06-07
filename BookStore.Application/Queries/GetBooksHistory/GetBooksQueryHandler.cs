using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory
{
   public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PagedList<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public async Task<PagedList<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
          var res= await _bookRepository.GetBooksAsync(request);
          return res.data;
        }
    }
}
