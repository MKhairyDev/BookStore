using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.UpdateBookCommand
{
   public class UpdateBookCommand:Book,IRequest<Response<Book>>
    {
    }
}
