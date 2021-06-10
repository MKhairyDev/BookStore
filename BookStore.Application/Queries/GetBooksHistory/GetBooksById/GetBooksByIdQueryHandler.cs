using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory.GetBooksById
{
   public class GetBooksByIdQueryHandler : IRequestHandler<GetBooksByIdQuery, Response<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<Response<BookDto>> Handle(GetBooksByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookWithAuthorAsync(request.Id);
            if (book == null) throw new ApiException($"Book Not Found.");
            var bookDto = _mapper.Map<BookDto>(book);
            return new Response<BookDto>(bookDto);
        }
    }
}
