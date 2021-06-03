using System;
using System.Threading;
using System.Threading.Tasks;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.UpdateBookCommand
{
   public class UpdateBookCommandHandler:IRequestHandler<UpdateBookCommand, Response<Book>>
    {
        public Task<Response<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
