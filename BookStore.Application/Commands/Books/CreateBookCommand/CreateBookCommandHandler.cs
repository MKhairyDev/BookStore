using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.CreateBookCommand
{
   public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<BookDto>>
    {
        private readonly IGenericCrudRepository<Book> _bookRepository;
        private readonly IGenericCrudRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IGenericCrudRepository<Book>bookRepository,IGenericCrudRepository<Author> authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository??throw  new ArgumentNullException(nameof(bookRepository));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        public async Task<Response<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var bookDomain = _mapper.Map<Book>(request);
            foreach (var author in request.Authors)
            {
              var authorExists= await _authorRepository.GetAsync(author.Id);
              if (authorExists != null)
              {
                  bookDomain.Authors.Add(authorExists);
              }
            }
            await  _bookRepository.AddAsync(bookDomain);
           await _bookRepository.SaveChangesAsync();
           var bookDto= _mapper.Map<BookDto>(bookDomain);
            return new Response<BookDto>(bookDto);
        }
    }
}
