using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.CreateBookCommand
{
   public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<Book>>
    {
        public Task<Response<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
