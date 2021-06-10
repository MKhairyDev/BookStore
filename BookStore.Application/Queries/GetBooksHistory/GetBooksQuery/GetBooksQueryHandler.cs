using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Parameters;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory.GetBooksQuery
{
   public class GetBooksQueryHandler : IRequestHandler<GetAllBooksQuery,Response<IEnumerable<BookDto>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        public async Task<Response<IEnumerable<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
          var bookList= await _bookRepository.GetAllBooksWithAuthorAsync();
          var bookDtoList = _mapper.Map<IEnumerable<BookDto>>(bookList);
          return new Response<IEnumerable<BookDto>>(bookDtoList);
        }
    }
}
